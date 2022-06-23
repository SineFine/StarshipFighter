using Unity.Entities;

namespace ECS.Components.Singletons
{
    public struct SpaceshipHealthComponent : IComponentData
    {
        public float MaxHealth;
        public float Health;
    }
}