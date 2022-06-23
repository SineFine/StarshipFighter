using System;
using ECS.Components.Singletons;
using ECSSystemEvent;
using Unity.Entities;

namespace ECS.Systems
{
    public struct HealthComponent
    {
        public float MaxValue;
        public float CurrentValue;
    }
    
    [UpdateBefore(typeof(DestroySystem))]
    public partial class SpaceshipHealthCheckSystem : SystemBase, ISystemValueChange<HealthComponent>
    {
        public event Action<HealthComponent> OnValueChange;
        
        private float _currentHealth;
        
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<SpaceshipHealthComponent>();
        }

        protected override void OnStartRunning()
        {
            _currentHealth = float.MaxValue;
        }

        protected override void OnUpdate()
        {
            var healthComponent = GetSingleton<SpaceshipHealthComponent>();

            if (!(_currentHealth > healthComponent.Health)) return;
            
            _currentHealth = healthComponent.Health;
            
            OnValueChange?.Invoke(new HealthComponent
            {
                CurrentValue = healthComponent.Health,
                MaxValue = healthComponent.MaxHealth
            });
        }

    }
}