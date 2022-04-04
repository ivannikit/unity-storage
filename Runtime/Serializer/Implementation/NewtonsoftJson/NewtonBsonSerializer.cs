#nullable enable
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;


namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public class NewtonBsonSerializer : NewtonsoftSerializer<byte[]>
    {
        public static NewtonBsonSerializer Create(JsonSerializerSettings settings) =>
            new NewtonBsonSerializer(settings);
        
        private NewtonBsonSerializer(JsonSerializerSettings settings) : base(settings)
        {
        }

        public override bool Deserialize(Type valueType, byte[] serializedValue, out object value)
        {
            using MemoryStream stream = new MemoryStream(serializedValue);
            return DeserializeFrom(stream, valueType, out value);
        }

        public override bool Serialize(object value, out byte[] serializedValue)
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

        public override bool DeserializeFrom(Stream stream, Type valueType, out object value)
        {
            try
            {
                JsonSerializerSettings settings = Settings();
                var serializer = JsonSerializer.CreateDefault(settings);
                using var streamReader = new BinaryReader(stream);
                using var jsonReader = new BsonReader(streamReader);
                value = serializer.Deserialize(jsonReader, valueType);
                return true;
            }
            catch
            {
                value = default!;
                return false;
            }
        }

        public override bool SerializeTo(Stream stream, object value)
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
