#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IRefTypeStorage<TAddress, TData>
    {
        RefTypeResource<TAddress, TData> CreateRefTypeResource(TAddress address);
    }
}
