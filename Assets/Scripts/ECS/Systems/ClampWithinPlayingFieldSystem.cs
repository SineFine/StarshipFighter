using ECS.Components.Singletons;
using ECS.Components.Tags;
using Unity.Entities;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateAfter(typeof(SpaceshipMoveSystem))]
    [UpdateAfter(typeof(EnemyMoveSystem))]
    [UpdateAfter(typeof(ArtilleryBulletMoveSystem))]
    public partial class ClampWithinPlayingFieldSystem : SystemBase
    {
        private PlayingFieldSizeComponent _points;
        
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<PlayingFieldSizeComponent>();
        }

        protected override void OnStartRunning()
        {
            _points = GetSingleton<PlayingFieldSizeComponent>();
        }

        protected override void OnUpdate()
        {
            var upperLeftPoint= _points.UpperLeftPoint;
            var lowerRightPoint= _points.LowerRightPoint;

            Entities
                .WithAll<ClampWithinPlayingFieldTagComponent>()
                .ForEach((ref Translation translation) =>
                {
                    var x = translation.Value.x;
                    var y = translation.Value.y;

                    if (upperLeftPoint.y < y)
                    {
                        translation.Value.y = upperLeftPoint.y;
                    }

                    if (upperLeftPoint.x > x)
                    {
                        translation.Value.x = upperLeftPoint.x;
                    }

                    if (lowerRightPoint.y > y)
                    {
                        translation.Value.y = lowerRightPoint.y;
                    }
                    
                    if (lowerRightPoint.x < x)
                    {
                        translation.Value.x = lowerRightPoint.x;
                    }
                    
                }).Run();
        }
    }
}