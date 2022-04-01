#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IDatabase<in TAddress, TData>
    {
        TData Pull(TAddress address);
        void Push(TAddress address, TData data);
    }
}
