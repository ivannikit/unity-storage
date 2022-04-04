#nullable enable
using System;
using Newtonsoft.Json;


namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public class NewtonJsonSerializer<TValueType> : NewtonsoftSerializer<TValueType, string>
    {
        public NewtonJsonSerializer(JsonSerializerSettings settings) : base(settings)
        {
        }

        public override bool Deserialize(string serializedValue, out object value)
        {
            Type type = ValueType();
            JsonSerializerSettings settings = Settings();
            try
            {
                value = JsonConvert.DeserializeObject(serializedValue, type, settings);
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
            JsonSerializerSettings settings = Settings();
            try
            {
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
