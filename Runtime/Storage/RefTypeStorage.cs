#nullable enable

namespace TeamZero.StorageSystem
{
    public class RefTypeStorage<TAddress, TData, TSerializeData> : IRefTypeStorage<TAddress, TData>
    {
        private readonly IDatabase<TAddress, TSerializeData> _database;
        private readonly ISerializer<TData, TSerializeData> _serializer;

        public static RefTypeStorage<TAddress, TData, TSerializeData> Create(
            IDatabase<TAddress, TSerializeData> database, ISerializer<TData, TSerializeData> serializer)
                => new RefTypeStorage<TAddress, TData, TSerializeData>(database, serializer);
        
        private RefTypeStorage(IDatabase<TAddress, TSerializeData> database, ISerializer<TData, TSerializeData> serializer)
        {
            _database = database;
            _serializer = serializer;
        }

        public IResource<TData> CreateRefTypeResource(TAddress address)
        => Resource<TAddress, TData, TSerializeData>.Create(address, _database, _serializer);
    }
}
