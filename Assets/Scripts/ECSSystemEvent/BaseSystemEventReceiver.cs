namespace ECSSystemEvent
{
    public abstract class BaseSystemEventReceiver
    {
        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}