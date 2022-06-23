using SO;
using Unity.Entities;

namespace ECS.Components.Singletons
{
    [GenerateAuthoringComponent]
    public struct ChosenWeaponComponent : IComponentData
    {
        public WeaponType WeaponType;
    }
}
