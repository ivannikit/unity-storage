using System.IO;

#nullable enable

namespace TeamZero.StorageSystem
{
    public abstract class FileSystemDatabase<TAddress> : IDatabase<TAddress, object>, IDatabase<TAddress, Stream>
    {
        public abstract bool Pull(TAddress address, out object data);
        public abstract bool Push(TAddress address, object data);

        public abstract bool Pull(TAddress address, out Stream data);
        public abstract bool Push(TAddress address, Stream data);
    }
}