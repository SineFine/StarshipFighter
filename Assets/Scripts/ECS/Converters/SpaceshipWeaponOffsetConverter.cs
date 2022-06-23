using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Converters
{
    public class SpaceshipWeaponOffsetConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private Transform _weaponPosition;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new SpaceshipWeaponOffsetComponent
            {
                Offset = _weaponPosition.localPosition
            });
        }
    }
}