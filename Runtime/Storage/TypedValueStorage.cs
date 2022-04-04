#nullable enable

namespace TeamZero.StorageSystem
{
    public class TypedValueStorage<TAddress, TData, TSerializeData> : IStorage<TAddress, TData>
    {
        private readonly IDatabase<TAddress, TSerializeData> _database;
        private readonly ITypedValueSerializer<TData, TSerializeData> _serializer;

        public static TypedValueStorage<TAddress, TData, TSerializeData> Create(
            IDatabase<TAddress, TSerializeData> database, ITypedValueSerializer<TData, TSerializeData> serializer)
                => new TypedValueStorage<TAddress, TData, TSerializeData>(database, serializer);
        
        private TypedValueStorage(IDatabase<TAddress, TSerializeData> database, 
            ITypedValueSerializer<TData, TSerializeData> serializer)
        {
            _database = database;
            _serializer = serializer;
        }

        public IResource<TData> CreateResource(TAddress address)
            => TypedValueResource<TAddress, TData, TSerializeData>.Create(address, _database, _serializer);
    }
}
