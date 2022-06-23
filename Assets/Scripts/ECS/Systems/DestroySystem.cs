using ECS.Components.Tags;
using Unity.Entities;

namespace ECS.Systems
{
    public partial class DestroySystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _buffer;
        
        protected override void OnCreate()
        {
            _buffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnUpdate()
        {
            var buffer = _buffer.CreateCommandBuffer();
            
            Entities
                .WithAll<DestroyTagComponent>()
                .ForEach((Entity entity) =>
                {
                    buffer.DestroyEntity(entity);
                }).Run();
        }
    }
}