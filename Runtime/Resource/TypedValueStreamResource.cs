#nullable enable
using System;
using System.IO;

namespace TeamZero.StorageSystem
{
    public class TypedValueStreamResource<TAddress, TData, TSerializeData> : IResource<TData>
    {
        private readonly TAddress _address;
        private readonly IStreamDatabase<TAddress> _database;
        private readonly ITypedValueStreamSerializer<TData> _serializer;

        public static TypedValueStreamResource<TAddress, TData, TSerializeData> Create(TAddress address, 
            IStreamDatabase<TAddress> database, ITypedValueStreamSerializer<TData> serializer)
                => new TypedValueStreamResource<TAddress, TData, TSerializeData>(address, database, serializer);
        
        private TypedValueStreamResource(TAddress address, IStreamDatabase<TAddress> database,
            ITypedValueStreamSerializer<TData> serializer)
        {
            _address = address;
            _database = database;
            _serializer = serializer;
        }

        public bool Pull(out TData data)
        {
            if (_database.CreatePullStream(_address, out Stream stream))
            {
                Type valueType = typeof(TData);
                return _serializer.DeserializeFrom(stream, valueType, out data);
            }

            data = default!;
            return false;
        }

        public bool Push(TData data)
        {
            bool result = false;
            if (_database.CreatePushStream(_address, out Stream stream))
                result = _serializer.SerializeTo(stream, data);

            return result;
        }
    }
}