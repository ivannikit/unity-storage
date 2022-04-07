#nullable enable

namespace TeamZero.StorageSystem
{
    public abstract class StreamStorage<TAddress, TData> : IStorage<TAddress, TData>
    {
        protected readonly IStreamDatabase<TAddress, TData> _database;
        protected readonly IStreamSerializer<TData> _serializer;
        
        protected StreamStorage(IStreamDatabase<TAddress, TData> database, IStreamSerializer<TData> serializer)
        {
            _database = database;
            _serializer = serializer;
        }

        public abstract IResource<TData> CreateResource(TAddress address);
    }
}
