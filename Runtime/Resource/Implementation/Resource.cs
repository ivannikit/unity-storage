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
            if (_database.Pull(_address, out TSerializeData serializedValue))
            {
                return _serializer.Deserialize(serializedValue, out data);
            }

            data = default!;
            return false;
        }

        public bool Push(TData data)
        {
            bool result = false;
            if(_serializer.Serialize(data, out TSerializeData serializedValue))
                result = _database.Push(_address, serializedValue);

            return result;
        }
    }
}