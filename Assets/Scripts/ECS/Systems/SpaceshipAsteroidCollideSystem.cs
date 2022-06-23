using ECS.Components;
using ECS.Components.Singletons;
using ECS.Components.Tags;
using ECS.Utils;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;

namespace ECS.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(ExportPhysicsWorld))]
    [UpdateBefore(typeof(EndFramePhysicsSystem))]
    [UpdateAfter(typeof(BulletEnemyCollidePhysicsSystem))]
    public partial class SpaceshipAsteroidCollideSystem : SystemBase
    {
        private StepPhysicsWorld _stepPhysicsWorld;
        private EndFixedStepSimulationEntityCommandBufferSystem _buffer;
        
        protected override void OnCreate()
        {
            _buffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        }

        [BurstCompile]
        private struct CollisionSystemJob : ICollisionEventsJob
        {
            public EntityCommandBuffer.ParallelWriter CommandBuffer;
            
            [ReadOnly] public ComponentDataFromEntity<EnemyTagComponent> EnemyGroup;
            [ReadOnly] public ComponentDataFromEntity<PlayerTagComponent> PlayerGroup;
            [ReadOnly] public ComponentDataFromEntity<EnemyDataComponent> EnemyDataGroup;
            [ReadOnly] public ComponentDataFromEntity<SpaceshipHealthComponent> SpaceshipHealtGroup;
            
            public void Execute(CollisionEvent collisionEvent)
            {
                if (!EcsUtils.CheckEntityCollisionByGroup(collisionEvent, EnemyGroup, PlayerGroup,
                        out var enemyEntity,
                        out var playerEntity)) return;

                var asteroidData = EnemyDataGroup[enemyEntity];
                var spaceshipData = SpaceshipHealtGroup[playerEntity];

                var spaceshipHealth = math.max(spaceshipData.Health - asteroidData.Damage, 0);

                if (spaceshipHealth == 0)
                {
                    CommandBuffer.AddComponent(0, playerEntity, new DestroyTagComponent());
                }
                
                CommandBuffer.AddComponent(0, playerEntity, new SpaceshipHealthComponent
                {
                    Health = spaceshipHealth,
                    MaxHealth = spaceshipData.MaxHealth
                });
                
                CommandBuffer.AddComponent(0, enemyEntity, new DestroyTagComponent());
            }
        }
        
        protected override void OnUpdate()
        {
            _stepPhysicsWorld.FinalSimulationJobHandle.Complete();
            
            var job = new CollisionSystemJob
            {
                CommandBuffer = _buffer.CreateCommandBuffer().AsParallelWriter(),
                EnemyGroup = GetComponentDataFromEntity<EnemyTagComponent>(),
                PlayerGroup = GetComponentDataFromEntity<PlayerTagComponent>(),
                EnemyDataGroup = GetComponentDataFromEntity<EnemyDataComponent>(),
                SpaceshipHealtGroup = GetComponentDataFromEntity<SpaceshipHealthComponent>()
            };

            Dependency = job.Schedule(_stepPhysicsWorld.Simulation, Dependency);
            
            Dependency.Complete();
        }
    }
}