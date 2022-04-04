#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IRefTypeStorage<in TAddress, TData>
    {
        IResource<TData> CreateRefTypeResource(TAddress address);
    }
}
