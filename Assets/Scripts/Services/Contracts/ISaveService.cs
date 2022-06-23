namespace Services.Contracts
{
    public interface ISaveService<in TData>
    {
        void Save(TData data);
    }
}