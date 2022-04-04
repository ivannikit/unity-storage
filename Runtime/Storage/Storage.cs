#nullable enable

namespace TeamZero.StorageSystem
{
    public class Storage<TAddress, TData, TSerializeData> : IStorage<TAddress, TData>
    {
        private readonly IDatabase<TAddress, TSerializeData> _database;
        private readonly ISerializer<TData, TSerializeData> _serializer;

        public static Storage<TAddress, TData, TSerializeData> Create(
            IDatabase<TAddress, TSerializeData> database, ISerializer<TData, TSerializeData> serializer)
                => new Storage<TAddress, TData, TSerializeData>(database, serializer);
        
        private Storage(IDatabase<TAddress, TSerializeData> database, ISerializer<TData, TSerializeData> serializer)
        {
            _database = database;
            _serializer = serializer;
        }

        public IResource<TData> CreateResource(TAddress address)
        => Resource<TAddress, TData, TSerializeData>.Create(address, _database, _serializer);
    }
}
