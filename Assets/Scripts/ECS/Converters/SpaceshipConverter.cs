using Unity.Entities;
using UnityEngine;

namespace ECS.Converters
{
    public class SpaceshipConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private GameObject _spaceship;
        public static Entity Spaceship { private set; get; }
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            Spaceship = GameObjectConversionUtility.ConvertGameObjectHierarchy(_spaceship,
                GameObjectConversionSettings.FromWorld(dstManager.World, conversionSystem.BlobAssetStore));
        }
    }
}