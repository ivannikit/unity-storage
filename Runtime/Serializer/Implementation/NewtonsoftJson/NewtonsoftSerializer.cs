#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Newtonsoft.Json;

namespace TeamZero.StorageSystem.NewtonsoftJson
{
    public abstract class NewtonsoftSerializer<TValue, TSerializedValue> : ISerializer<TValue, TSerializedValue>, IStreamSerializer<TValue>
    {
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
        
        public abstract bool Deserialize(Type valueType, TSerializedValue serializedValue, out TValue value);
        public abstract bool Serialize(TValue value, [NotNullWhen(true)] out TSerializedValue serializedValue);


        public abstract bool DeserializeFrom(Stream stream, Type valueType, out TValue value);
        public abstract bool SerializeTo(Stream stream, TValue value);
    }
}
