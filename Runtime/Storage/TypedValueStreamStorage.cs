#nullable enable

namespace TeamZero.StorageSystem
{
    public class TypedValueStreamStorage<TAddress, TData, TSerializeData> : IStorage<TAddress, TData>
    {
        private readonly IStreamDatabase<TAddress> _database;
        private readonly ITypedValueStreamSerializer<TData> _serializer;

        public static TypedValueStreamStorage<TAddress, TData, TSerializeData> Create(
            IStreamDatabase<TAddress> database, ITypedValueStreamSerializer<TData> serializer)
                => new TypedValueStreamStorage<TAddress, TData, TSerializeData>(database, serializer);
        
        private TypedValueStreamStorage(IStreamDatabase<TAddress> database, ITypedValueStreamSerializer<TData> serializer)
        {
            _database = database;
            _serializer = serializer;
        }

        public IResource<TData> CreateResource(TAddress address)
            => TypedValueStreamResource<TAddress, TData, TSerializeData>.Create(address, _database, _serializer);
    }
}
