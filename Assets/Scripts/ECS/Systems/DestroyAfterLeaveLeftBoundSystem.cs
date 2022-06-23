using ECS.Components.Singletons;
using ECS.Components.Tags;
using Unity.Entities;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateBefore(typeof(DestroySystem))]
    public partial class DestroyAfterLeaveLeftBoundSystem : SystemBase
    {
        private PlayingFieldSizeComponent _removePosition;
        
        private EndSimulationEntityCommandBufferSystem _buffer;

        protected override void OnCreate()
        {
            _buffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            _removePosition = GetSingleton<PlayingFieldSizeComponent>();
        }

        protected override void OnUpdate()
        {
            var buffer = _buffer.CreateCommandBuffer();
            var position = _removePosition.LowerLeftPoint;
            
            Entities
                .WithAll<DestroyAfterLeaveLeftBound>()
                .ForEach((Entity entity, in Translation translation) =>
                {
                    if (position.x < translation.Value.x) return;
                    
                    buffer.AddComponent<DestroyTagComponent>(entity);
                }).Run();
        }
    }
}