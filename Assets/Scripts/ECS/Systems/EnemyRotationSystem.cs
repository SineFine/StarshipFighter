using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateAfter(typeof(EnemySpawnSystem))]
    public partial class EnemyRotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            
            Entities.ForEach((ref Rotation rotation, in EnemyDataComponent rotateData) =>
            {
                rotation.Value = math.mul(rotation.Value, quaternion.Euler(0, 0, math.radians(rotateData.RotationSpeed) * deltaTime));
            }).Run();
        }
    }
}