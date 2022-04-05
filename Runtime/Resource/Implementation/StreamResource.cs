#nullable enable

namespace TeamZero.StorageSystem
{
    public class StreamResource<TAddress, TData> : IResource<TData>
    {
        private readonly TAddress _address;
        private readonly IStreamDatabase<TAddress, TData> _database;
        private readonly IStreamSerializer<TData> _serializer;

        public static StreamResource<TAddress, TData> Create(
            TAddress address, 
            IStreamDatabase<TAddress, TData> database, 
            IStreamSerializer<TData> serializer)
                => new StreamResource<TAddress, TData>(address, database, serializer);
        
        private StreamResource(
            TAddress address, 
            IStreamDatabase<TAddress, TData> database,
            IStreamSerializer<TData> serializer)
        {
            _address = address;
            _database = database;
            _serializer = serializer;
        }

        public bool Pull(out TData data) => _database.Pull(_address, typeof(TData), _serializer, out data);

        public bool Push(TData data) => _database.Push(_address, _serializer, data);
    }
}