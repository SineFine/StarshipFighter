using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Components.Singletons
{
    public struct PlayingFieldSizeComponent : IComponentData
    {
        public float3 UpperRightPoint;
        public float3 LowerRightPoint;
        public float3 UpperLeftPoint;
        public float3 LowerLeftPoint;
    }
}
