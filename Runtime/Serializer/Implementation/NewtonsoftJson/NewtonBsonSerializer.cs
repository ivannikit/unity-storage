#nullable enable
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;


namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public class NewtonBsonSerializer<TValue> : NewtonsoftSerializer<TValue, byte[]>
    {
        public static NewtonBsonSerializer<TValue> Create(JsonSerializerSettings settings) =>
            new NewtonBsonSerializer<TValue>(settings);
        
        private NewtonBsonSerializer(JsonSerializerSettings settings) : base(settings)
        {
        }

        public override bool Deserialize(Type valueType, byte[] serializedValue, out TValue value)
        {
            using MemoryStream stream = new MemoryStream(serializedValue);
            return DeserializeFrom(stream, valueType, out value);
        }

        public override bool Serialize(TValue value, out byte[] serializedValue)
        {
            using MemoryStream stream = new MemoryStream();
            if (SerializeTo(stream, value))
            {
                serializedValue = stream.ToArray();
                return true;
            }
            
            serializedValue = default!;
            return false;
        }

        public override bool DeserializeFrom(Stream stream, Type valueType, out TValue value)
        {
            try
            {
                JsonSerializerSettings settings = Settings();
                var serializer = JsonSerializer.CreateDefault(settings);
                using var streamReader = new BinaryReader(stream);
                using var jsonReader = new BsonReader(streamReader);
                value = (TValue)serializer.Deserialize(jsonReader, valueType);
                return true;
            }
            catch
            {
                value = default!;
                return false;
            }
        }

        public override bool SerializeTo(Stream stream, TValue value)
        {
            try
            {
                JsonSerializerSettings settings = Settings();
                var serializer = JsonSerializer.CreateDefault(settings);
                using var streamWriter = new BinaryWriter(stream);
                using var jsonWriter = new BsonWriter(streamWriter);
                serializer.Serialize(jsonWriter, value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
