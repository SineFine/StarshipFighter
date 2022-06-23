using System;

namespace ECSSystemEvent
{
    public interface ISystemValueChange<out TData>
    {
        event Action<TData> OnValueChange;
    }
}