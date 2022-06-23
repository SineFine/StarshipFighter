using Unity.Entities;

namespace ECS.Components
{
    public struct EnemyDataComponent : IComponentData
    {
        public float MoveSpeed;
        public float RotationSpeed;
        public float Damage;
        public float Hp;
    }
}