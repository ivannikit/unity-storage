#nullable enable

namespace TeamZero.StorageSystem
{
    public class SerializeObjectResource<TAddress, TData> : IResource<TData>
    {
        private readonly TAddress _address;
        private readonly IStreamDatabase<TAddress, object> _database;
        private readonly IStreamSerializer<object> _serializer;

        public static SerializeObjectResource<TAddress, TData> Create(
            TAddress address, 
            IStreamDatabase<TAddress, object> database, 
            IStreamSerializer<object> serializer)
                => new SerializeObjectResource<TAddress, TData>(address, database, serializer);
        
        private SerializeObjectResource(
            TAddress address, 
            IStreamDatabase<TAddress, object> database,
            IStreamSerializer<object> serializer)
        {
            _address = address;
            _database = database;
            _serializer = serializer;
        }

        public bool Pull(out TData data)
        {
            if (_database.Pull(_address, typeof(TData), _serializer, out object deserializedData))
            {
                data = (TData)deserializedData;
                return data != null;
            }

            data = default!;
            return false;
        }

        public bool Push(TData data)
        {
            object? obj = data;
            if(obj != null)
                return _database.Push(_address, _serializer, obj);

            return false;
        }
    }
}