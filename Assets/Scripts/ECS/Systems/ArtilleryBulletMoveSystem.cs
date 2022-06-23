using ECS.Components;
using ECS.Components.Tags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateAfter(typeof(SpaceshipArtillerySpawnSystem))]
    public partial class ArtilleryBulletMoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var time = Time.DeltaTime;
            
            Entities
                .WithAll<ArtilleryBulletTagComponent>()
                .ForEach((ref Translation translation, in BulletDataComponent data) =>
            {
                var distance = math.right() * data.Speed * time;

                translation.Value += distance;
            }).Run();
        }
    }
}