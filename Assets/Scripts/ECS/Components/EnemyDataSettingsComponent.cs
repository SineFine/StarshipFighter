using Unity.Entities;

namespace ECS.Components
{
    public class EnemyDataSettingsComponent : IComponentData
    {
        public float MaximumHp;
        public float MinMoveSpeed;
        public float MaxMoveSpeed;
        public float MinRotationSpeed;
        public float MaxRotationSpeed;
        public float Damage;
    }
}