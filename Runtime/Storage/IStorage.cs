#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IStorage<in TAddress, TData>
    {
        IResource<TData> CreateResource(TAddress address);
    }
}
