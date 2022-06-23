using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateAfter(typeof(InputSystem))]
    public partial class SpaceshipMoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            
            Entities.ForEach((ref Translation translation, in SpaceshipMoveDataComponent move) =>
            {
                var direction = math.normalizesafe(move.Directions);

                direction *= deltaTime * move.Speed;
                
                translation.Value += direction;

            }).Run();
        }
    }
}