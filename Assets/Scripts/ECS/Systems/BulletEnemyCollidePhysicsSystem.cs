using ECS.Components;
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
    public partial class BulletEnemyCollidePhysicsSystem : SystemBase
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
            [ReadOnly] public ComponentDataFromEntity<BulletTagComponent> BulletGroup;
            [ReadOnly] public ComponentDataFromEntity<EnemyTagComponent> EnemyGroup;
            [ReadOnly] public ComponentDataFromEntity<BulletDataComponent> BulletDataGroup;
            [ReadOnly] public ComponentDataFromEntity<EnemyDataComponent> EnemyDataGroup;

            public void Execute(CollisionEvent collisionEvent)
            {
                if (!EcsUtils.CheckEntityCollisionByGroup(collisionEvent, BulletGroup, EnemyGroup, 
                        out var bulletEntity,
                        out var enemyEntity)) return;

                var bulletData = BulletDataGroup[bulletEntity];

                if (bulletData.DestroyOnImpact)
                {
                    CommandBuffer.DestroyEntity(0, bulletEntity);
                }

                var asteroidData = EnemyDataGroup[enemyEntity];

                var hpValue = math.max(asteroidData.Hp - bulletData.Damage, 0);

                if (hpValue == 0)
                {
                    CommandBuffer.AddComponent<DestroyByPlayerTagComponent>(0, enemyEntity);
                    CommandBuffer.AddComponent<DestroyTagComponent>(0, enemyEntity);
                }
                else
                {
                    CommandBuffer.SetComponent(0, enemyEntity, new EnemyDataComponent
                    {
                        Damage = asteroidData.Damage,
                        Hp = hpValue,
                        MoveSpeed = asteroidData.MoveSpeed,
                        RotationSpeed = asteroidData.RotationSpeed
                    });
                }
                
            }
        }
        
        protected override void OnUpdate()
        {
            _stepPhysicsWorld.FinalSimulationJobHandle.Complete();
            
            var job = new CollisionSystemJob
            {
                CommandBuffer = _buffer.CreateCommandBuffer().AsParallelWriter(),
                BulletGroup = GetComponentDataFromEntity<BulletTagComponent>(),
                EnemyGroup = GetComponentDataFromEntity<EnemyTagComponent>(),
                BulletDataGroup = GetComponentDataFromEntity<BulletDataComponent>(),
                EnemyDataGroup = GetComponentDataFromEntity<EnemyDataComponent>()
            };

            Dependency = job.Schedule(_stepPhysicsWorld.Simulation, Dependency);
            
            Dependency.Complete();
        }
    }
}