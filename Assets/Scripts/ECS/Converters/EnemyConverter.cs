using ECS.Components;
using SO;
using Unity.Entities;
using UnityEngine;

namespace ECS.Converters
{
    public class EnemyConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private EnemyPrefsSo[] _asteroids;

        public static Entity[] AsteroidsEntities { private set; get; }
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            AsteroidsEntities = new Entity[_asteroids.Length];

            for (var i = 0; i < _asteroids.Length; i++)
            {
                var asteroidSo = _asteroids[i];
                
                var asteroidEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroidSo.EnemyPrefab,
                    GameObjectConversionSettings.FromWorld(dstManager.World, conversionSystem.BlobAssetStore));

                AsteroidsEntities[i] = asteroidEntity;

                dstManager.AddComponentData(asteroidEntity, new EnemyDataSettingsComponent
                {
                    Damage = asteroidSo.Damage,
                    MaximumHp = asteroidSo.MaximumHP,
                    MaxMoveSpeed = asteroidSo.MaxMoveSpeed,
                    MinMoveSpeed = asteroidSo.MinMoveSpeed,
                    MinRotationSpeed = asteroidSo.MinRotationSpeed,
                    MaxRotationSpeed = asteroidSo.MaxRotationSpeed
                });

                dstManager.AddComponentData(asteroidEntity, new PointForKillComponent
                {
                    Points = asteroidSo.VictoryPoint
                });
            }
        }
    }
}