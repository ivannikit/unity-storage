#nullable enable
using System;
using System.IO;
using Newtonsoft.Json;


namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public class NewtonJsonSerializer<TValue> : NewtonsoftSerializer<TValue, string>
    {
        private static JsonSerializerSettings DefaultSettings() 
            => new JsonSerializerSettings { Formatting = Formatting.Indented };
        
        public static NewtonJsonSerializer<TValue> Create() 
            => new NewtonJsonSerializer<TValue>(DefaultSettings());
        
        public static NewtonJsonSerializer<TValue> Create(JsonSerializerSettings settings) =>
            new NewtonJsonSerializer<TValue>(settings);
        
        private NewtonJsonSerializer(JsonSerializerSettings settings) : base(settings)
        {
        }

        public override bool Deserialize(Type valueType, string serializedValue, out TValue value)
        {
            try
            {
                JsonSerializerSettings settings = Settings();
                value = (TValue)JsonConvert.DeserializeObject(serializedValue, valueType, settings);
                return true;
            }
            catch
            {
                value = default!;
                return false;
            }
        }

        public override bool Serialize(TValue value, out string serializedValue)
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

        public override bool DeserializeFrom(Stream stream, Type valueType, out TValue value)
        {
            try
            {
                JsonSerializerSettings settings = Settings();
                var serializer = JsonSerializer.CreateDefault(settings);
                using var streamReader = new StreamReader(stream);
                using var jsonReader = new JsonTextReader(streamReader);
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
