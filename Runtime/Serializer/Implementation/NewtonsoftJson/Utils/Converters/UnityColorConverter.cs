using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Newtonsoft.Json
{
    public class UnityColorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Color);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            Color color = new Color(
                (float)obj.GetValue("r"),
                (float)obj.GetValue("g"),
                (float)obj.GetValue("b"),
                (float)obj.GetValue("a")
                );

            return color;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Color color = (Color)value;

            JObject t = new JObject();
            t.Add("r", new JValue(color.r));
            t.Add("g", new JValue(color.g));
            t.Add("b", new JValue(color.b));
            t.Add("a", new JValue(color.a));
            t.WriteTo(writer);
        }
    }
}
