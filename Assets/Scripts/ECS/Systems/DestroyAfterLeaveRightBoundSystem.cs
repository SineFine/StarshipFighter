using ECS.Components.Singletons;
using ECS.Components.Tags;
using Unity.Entities;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateBefore(typeof(DestroySystem))]
    public partial class DestroyAfterLeaveRightBoundSystem : SystemBase
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
            var boundPosition = _removePosition.LowerRightPoint;
            
            Entities
                .WithAll<DestroyAfterLeaveRightBound>()
                .ForEach((Entity entity, in Translation translation) =>
                {
                    if ( translation.Value.x < boundPosition.x) return;
                    
                    buffer.AddComponent<DestroyTagComponent>(entity);
                }).Run();
        }
    }
}