#nullable enable

namespace TeamZero.StorageSystem
{
    public class Resource<TAddress, TData, TSerializeData> : IResource<TData>
    {
        private readonly TAddress _address;
        private readonly IDatabase<TAddress, TSerializeData> _database;
        private readonly ISerializer<TData, TSerializeData> _serializer;

        public static Resource<TAddress, TData, TSerializeData> Create(TAddress address, 
            IDatabase<TAddress, TSerializeData> database, ISerializer<TData, TSerializeData> serializer)
                => new Resource<TAddress, TData, TSerializeData>(address, database, serializer);
        
        private Resource(TAddress address, IDatabase<TAddress, TSerializeData> database,
            ISerializer<TData, TSerializeData> serializer)
        {
            _address = address;
            _database = database;
            _serializer = serializer;
        }

        public bool Pull(out TData data)
        {
            TSerializeData serializedValue = _database.Pull(_address);
            return _serializer.Deserialize(serializedValue, out data);
        }

        public void Push(TData data)
        {
            bool serializationComplete = _serializer.Serialize(data, out TSerializeData serializedValue);
            if(serializationComplete)
                _database.Push(_address, serializedValue);
        }
    }
}