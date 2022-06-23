using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct SpaceshipRotateDataComponent : IComponentData
    {
        public float3 StartedAngle;
        public float3 Deviation;
    }
}