using System;
using System.Collections.Generic;
using Newtonsoft.Json;

#nullable enable

namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public abstract class NewtonsoftSerializer<TDataType, TSerializedValue> : ISerializer<object, TSerializedValue>
    {
        public Type DataType() => typeof(TDataType);
        
        private readonly JsonSerializerSettings _settings;

        protected NewtonsoftSerializer(JsonSerializerSettings settings)
        {
            SetupExtraSettings(settings);
            _settings = settings;
        }

        private static void SetupExtraSettings(JsonSerializerSettings settings)
        {
            IList<JsonConverter> converters = settings.Converters;
            bool needToInject = true;
            foreach (JsonConverter converter in converters)
            {
                Type type = converter.GetType();
                if (type == typeof(UnityColorConverter))
                {
                    needToInject = false;
                    break;
                }
            }
            
            if(needToInject)
                converters.Add(new UnityColorConverter());
        }
        

        protected JsonSerializerSettings Settings() => _settings;
        public abstract bool Deserialize(TSerializedValue serializedValue, out object value);
        public abstract bool Serialize(object value, out TSerializedValue serializedValue);
    }
}
