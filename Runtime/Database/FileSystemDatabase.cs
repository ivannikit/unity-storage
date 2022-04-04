#nullable enable

namespace TeamZero.StorageSystem
{
    public abstract class FileSystemDatabase<TAddress> : IDatabase<TAddress, object>
    {
        public abstract object Pull(TAddress address);
        public abstract void Push(TAddress address, object data);
    }
}