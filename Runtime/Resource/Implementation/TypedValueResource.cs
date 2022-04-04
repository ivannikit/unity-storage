using System;

#nullable enable

namespace TeamZero.StorageSystem
{
    public class TypedValueResource<TAddress, TData, TSerializeData> : IResource<TData>
    {
        private readonly TAddress _address;
        private readonly IDatabase<TAddress, TSerializeData> _database;
        private readonly ITypedValueSerializer<TData, TSerializeData> _serializer;

        public static TypedValueResource<TAddress, TData, TSerializeData> Create(TAddress address, 
            IDatabase<TAddress, TSerializeData> database, ITypedValueSerializer<TData, TSerializeData> serializer)
                => new TypedValueResource<TAddress, TData, TSerializeData>(address, database, serializer);
        
        private TypedValueResource(TAddress address, IDatabase<TAddress, TSerializeData> database,
            ITypedValueSerializer<TData, TSerializeData> serializer)
        {
            _address = address;
            _database = database;
            _serializer = serializer;
        }

        public bool Pull(out TData data)
        {
            if (_database.Pull(_address, out TSerializeData serializedValue))
            {
                Type valueType = typeof(TData);
                return _serializer.Deserialize(valueType, serializedValue, out data);
            }

            data = default!;
            return false;
        }

        public bool Push(TData data)
        {
            bool result = false;
            if (_serializer.Serialize(data, out TSerializeData serializedValue))
                result = _database.Push(_address, serializedValue);

            return result;
        }
    }
}