using Controllers.Contracts;
using ECS.Systems;
using ECSSystemEvent;
using Unity.Entities;

namespace Controllers
{
    public class HealthController : SystemEventReceiver<HealthComponent>
    {
        private readonly IHeathView _heathView;

        public HealthController(IHeathView heathView)
            : base(World.DefaultGameObjectInjectionWorld.GetExistingSystem<SpaceshipHealthCheckSystem>())
        {
            _heathView = heathView;
        }

        protected override void OnValueChange(HealthComponent healthComponent)
        {
            _heathView.SetupHealth(healthComponent.MaxValue, healthComponent.CurrentValue);
        }
    }
}