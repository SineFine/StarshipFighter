using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Components
{
    public struct SpaceshipWeaponOffsetComponent : IComponentData
    {
        public float3 Offset;
    }
}