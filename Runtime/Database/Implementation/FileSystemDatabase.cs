#nullable enable
using System.IO;

namespace TeamZero.StorageSystem
{
    public abstract class FileSystemDatabase<TAddress> : IDatabase<TAddress, object>, IStreamDatabase<TAddress>
    {
        public abstract bool Pull(TAddress address, out object data);
        public abstract bool Push(TAddress address, object data);


        public abstract bool CreatePullStream(TAddress address, out Stream stream);
        public abstract bool CreatePushStream(TAddress address, out Stream stream);
    }
}