using System;
using ECS.Components.Tags;
using ECSSystemEvent;
using Unity.Entities;

namespace ECS.Systems
{
    [UpdateBefore(typeof(DestroySystem))]
    public partial class SpaceshipCheckerSystem : SystemBase, ISystemValueChange<bool>
    {
        public event Action<bool> OnValueChange;
        
        protected override void OnUpdate()
        {
            Entities
                .WithAll<PlayerTagComponent>()
                .WithAll<DestroyTagComponent>().ForEach((Entity entity) =>
                {
                }).Run();

            OnValueChange?.Invoke(true);
        }
    }
}