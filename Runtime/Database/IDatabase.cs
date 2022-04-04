#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IDatabase<in TAddress, TData>
    {
        bool Pull(TAddress address, out TData data);
        bool Push(TAddress address, TData data);
    }
}
