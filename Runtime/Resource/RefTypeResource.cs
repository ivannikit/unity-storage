#nullable enable

namespace TeamZero.StorageSystem
{
    public abstract class RefTypeResource<TAddress, TData>
    {
        public abstract TData Pull(TAddress address);
        public abstract void Push();
    }
}