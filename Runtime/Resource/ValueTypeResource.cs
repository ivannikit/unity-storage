#nullable enable

namespace TeamZero.StorageSystem
{
    public abstract class ValueTypeResource<TAddress, TData>
    {
        public abstract ref readonly TData Pull(TAddress address);
        public abstract void Push(TAddress address, in TData data);
    }
}