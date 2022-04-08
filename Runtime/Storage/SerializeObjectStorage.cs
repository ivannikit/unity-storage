#nullable enable

namespace TeamZero.StorageSystem
{
    public abstract class SerializeObjectStorage<TAddress, TData> : IStorage<TAddress, TData>
    {
        protected readonly IStreamDatabase<TAddress, object> _database;
        protected readonly IStreamSerializer<object> _serializer;
        
        protected SerializeObjectStorage(IStreamDatabase<TAddress, object> database, IStreamSerializer<object> serializer)
        {
            _database = database;
            _serializer = serializer;
        }

        public abstract IResource<TData> CreateResource(TAddress address);
    }
}
