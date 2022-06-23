using SO;
using Unity.Entities;

namespace ECS.Components
{
    public struct BulletDataComponent : IComponentData
    {
        public float Speed;
        public float Damage;
        public bool DestroyOnImpact;
        public WeaponType WeaponType;
    }
}