using ECS.Components;
using ECS.Components.Singletons;
using ECS.Components.Tags;
using ECS.Converters;
using SO;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateAfter(typeof(InputSystem))]
    public partial class SpaceshipArtillerySpawnSystem : SystemBase
    {
        private Entity _weaponEntity;
        private float _fireTime;
         
        private BeginSimulationEntityCommandBufferSystem _buffer;

        protected override void OnCreate()
        {
            _fireTime = 0.0f;
            _buffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
            
            RequireSingletonForUpdate<ChosenWeaponComponent>();
            RequireSingletonForUpdate<SpaceshipFireDataComponent>();
        }

        protected override void OnStartRunning()
        {
            _weaponEntity = SpaceshipWeaponConverter.Weapons[WeaponType.Artillery];
        }

        protected override void OnUpdate()
        {
            _fireTime = math.max(_fireTime - Time.DeltaTime, 0.0f);
            
            var chosenWeapon = GetSingleton<ChosenWeaponComponent>();

            if (chosenWeapon.WeaponType != WeaponType.Artillery) return;

            var fireDataComponent = GetSingleton<SpaceshipFireDataComponent>();

            var isNeedShot = fireDataComponent.IsPressed || fireDataComponent.IsHold;

            if (!isNeedShot || _fireTime > 0.0f) return;
            
            _fireTime = 0.5f;
            
            var commandBuffer = _buffer.CreateCommandBuffer();
            var weaponEntity = _weaponEntity;

            Entities
                .WithAll<PlayerTagComponent>()
                .ForEach((in Translation translation, in SpaceshipRotateDataComponent rotate, in SpaceshipWeaponOffsetComponent offset) =>
                {
                    var entity = commandBuffer.Instantiate(weaponEntity);
                    
                    commandBuffer.SetComponent(entity, new Rotation
                    {
                        Value = quaternion.Euler(math.radians(rotate.StartedAngle))
                    });
                        
                    commandBuffer.SetComponent(entity, new Translation
                    {
                        Value = new float3 
                        { 
                            x = translation.Value.x + offset.Offset.z, 
                            y = translation.Value.y, 
                            z = translation.Value.z 
                        }
                    });
                    
                    commandBuffer.AddComponent(entity, new ArtilleryBulletTagComponent());
                    commandBuffer.AddComponent(entity, new DestroyAfterLeaveRightBound());
                    commandBuffer.AddComponent<DestroyWhenResetTag>(entity);
                }).Run();
        }
    }
}