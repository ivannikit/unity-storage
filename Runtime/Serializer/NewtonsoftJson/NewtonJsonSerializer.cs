#nullable enable
using System;
using Newtonsoft.Json;


namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public class NewtonJsonSerializer : NewtonsoftSerializer<string>
    {
        public static NewtonJsonSerializer Create(JsonSerializerSettings settings) =>
            new NewtonJsonSerializer(settings);
        
        public NewtonJsonSerializer(JsonSerializerSettings settings) : base(settings)
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

        /*public object Deserialize(Stream stream, Type type)
        {
            JsonSerializerSettings settings = Settings();
            var serializer = JsonSerializer.CreateDefault(settings);

            using (var sr = new StreamReader(stream))
                using (var jr = new JsonTextReader(sr))
                {
                    return serializer.Deserialize(jr, type);
                }
        }*/
    }
}
