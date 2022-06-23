using Unity.Entities;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct PointForKillComponent : IComponentData
    {
        public int Points;
    }
}