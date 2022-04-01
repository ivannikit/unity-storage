#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IValueTypeStorage<TAddress, TData>
    {
        ValueTypeResource<TAddress, TData> CreateValueTypeResource(TAddress address);
    }
}
