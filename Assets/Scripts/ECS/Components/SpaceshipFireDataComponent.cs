using Unity.Entities;

namespace ECS.Components
{
    [GenerateAuthoringComponent]
    public struct SpaceshipFireDataComponent : IComponentData
    {
        public bool IsPressed;
        public bool IsHold;
    }
}