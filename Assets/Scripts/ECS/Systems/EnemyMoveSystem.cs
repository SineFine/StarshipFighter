using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateAfter(typeof(EnemySpawnSystem))]
    public partial class EnemyMoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var time = Time.DeltaTime;
            
            Entities.ForEach((ref Translation translation, in EnemyDataComponent data) =>
            {
                var distance = math.left() * data.MoveSpeed * time;

                translation.Value += distance;
            }).Run();
        }
    }
}