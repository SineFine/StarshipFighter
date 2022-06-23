namespace Services.Contracts
{
    public interface ILoadService<out TData>
    {
        TData Load();
    }
}