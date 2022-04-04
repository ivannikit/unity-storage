#nullable enable
using System;
using System.IO;
using Newtonsoft.Json;


namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public class NewtonJsonSerializer : NewtonsoftSerializer<string>
    {
        public static NewtonJsonSerializer Create(JsonSerializerSettings settings) =>
            new NewtonJsonSerializer(settings);
        
        private NewtonJsonSerializer(JsonSerializerSettings settings) : base(settings)
        {
        }

        public override bool Deserialize(Type valueType, string serializedValue, out object value)
        {
            try
            {
                JsonSerializerSettings settings = Settings();
                value = JsonConvert.DeserializeObject(serializedValue, valueType, settings);
                return true;
            }
            catch
            {
                value = default!;
                return false;
            }
        }

        public override bool Serialize(object value, out string serializedValue)
        {
            try
            {
                JsonSerializerSettings settings = Settings();
                serializedValue = JsonConvert.SerializeObject(value, settings);
                return true;
            }
            catch
            {
                serializedValue = default!;
                return false;
            }
        }

        public override bool DeserializeFrom(Stream stream, Type valueType, out object value)
        {
            try
            {
                JsonSerializerSettings settings = Settings();
                var serializer = JsonSerializer.CreateDefault(settings);
                using var streamReader = new StreamReader(stream);
                using var jsonReader = new JsonTextReader(streamReader);
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
                using var streamWriter = new StreamWriter(stream);
                using var jsonWriter = new JsonTextWriter(streamWriter);
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
