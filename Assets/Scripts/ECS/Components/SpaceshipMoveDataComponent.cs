using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct SpaceshipMoveDataComponent : IComponentData
    {
        public float3 Directions;
        public float Speed;
    }
}