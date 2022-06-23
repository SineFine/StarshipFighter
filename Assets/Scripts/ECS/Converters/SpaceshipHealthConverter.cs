using ECS.Components.Singletons;
using Unity.Entities;
using UnityEngine;

namespace ECS.Converters
{
    public class SpaceshipHealthConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float _maxHealth;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new SpaceshipHealthComponent
            {
                Health = _maxHealth,
                MaxHealth = _maxHealth
            });
        }
    }
}