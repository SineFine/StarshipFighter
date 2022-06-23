using ECS.Components.Singletons;
using Unity.Entities;
using UnityEngine;

namespace ECS.Converters
{
    public class EnemySpawnPointsConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private Camera _camera;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var upperRightPoint = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
            var lowerRightPoint = _camera.ViewportToWorldPoint(new Vector3(1, 0, _camera.nearClipPlane));
            var upperLeftPoint = _camera.ViewportToWorldPoint(new Vector3(0, 1, _camera.nearClipPlane));
            var lowerLeftPoint = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));

            upperRightPoint.z = 0;
            lowerRightPoint.z = 0;

            dstManager.AddComponentData(entity, new PlayingFieldSizeComponent
            {
                UpperRightPoint = upperRightPoint,
                LowerRightPoint = lowerRightPoint,
                UpperLeftPoint = upperLeftPoint,
                LowerLeftPoint = lowerLeftPoint
            });
        }
    }
}