using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateAfter(typeof(InputSystem))]
    public partial class SpaceshipRotateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Rotation rotation, in SpaceshipRotateDataComponent rotateData, in SpaceshipMoveDataComponent moveData) =>
            {
                var from = quaternion.Euler(math.radians(rotateData.StartedAngle));
                var to = quaternion.Euler(math.radians(rotateData.Deviation));

                var angle =  math.slerp(from, to, moveData.Directions.y);

                rotation.Value = angle;
            }).Run();
        }
    }
}