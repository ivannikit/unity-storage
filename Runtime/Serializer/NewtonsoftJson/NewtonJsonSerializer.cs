/*
#nullable enable
using System;
using System.IO;
using Newtonsoft.Json;


namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public class NewtonJsonSerializer<TDataType> : NewtonsoftSerializer<TDataType, string>
    {
        public NewtonJsonSerializer(JsonSerializerSettings settings) : base(settings)
        {
        }

        public override bool Deserialize(string serializedValue, out object value)
        {
            throw new System.NotImplementedException();
        }

        public override bool Serialize(object value, out string serializedValue)
        {
            Type type = DataType();
            JsonSerializerSettings settings = Settings();
            return JsonConvert.DeserializeObject(value, type, settings);
        }
        
        public void Serialize(Stream stream, object value)
        {
            JsonSerializerSettings settings = Settings();
            var serializer = JsonSerializer.CreateDefault(settings);

            using (var sw = new StreamWriter(stream))
                using (var jw = new JsonTextWriter(sw))
                    serializer.Serialize(jw, value);
            
        }

        public object Deserialize(Stream stream, Type type)
        {
            JsonSerializerSettings settings = Settings();
            var serializer = JsonSerializer.CreateDefault(settings);

            using (var sr = new StreamReader(stream))
                using (var jr = new JsonTextReader(sr))
                {
                    return serializer.Deserialize(jr, type);
                }
        }

        private string Serialize(object value)
        {
            JsonSerializerSettings settings = Settings();
            return JsonConvert.SerializeObject(value, settings);
        }

        private object Deserialize(string value, Type type)
        {
            JsonSerializerSettings settings = Settings();
            return JsonConvert.DeserializeObject(value, type, settings);
        }
    }
}
*/
