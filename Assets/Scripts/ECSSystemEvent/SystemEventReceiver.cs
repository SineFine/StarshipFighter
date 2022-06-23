namespace ECSSystemEvent
{
    public abstract class SystemEventReceiver<TData> : BaseSystemEventReceiver
    {
        private readonly ISystemValueChange<TData> _system;

        protected SystemEventReceiver(ISystemValueChange<TData> system)
        {
            _system = system;
        }

        protected abstract void OnValueChange(TData data);

        public override void Subscribe()
        {
            _system.OnValueChange += OnValueChange;
        }

        public override void Unsubscribe()
        {
            _system.OnValueChange -= OnValueChange;
        }
    }
}