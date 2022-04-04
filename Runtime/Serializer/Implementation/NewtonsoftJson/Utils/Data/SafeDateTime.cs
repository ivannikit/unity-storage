using System;

namespace Newtonsoft.Json
{
    public struct SafeDateTime
    {
        [JsonProperty("ticks")]
        private long _ticks;
        
        [JsonProperty("kind")]
        private DateTimeKind _kind;

        public DateTime GetValue()
        {
            return new DateTime(_ticks, _kind);
        }

        [JsonConstructor]
        private SafeDateTime(long ticks, DateTimeKind kind)
        {
            _ticks = ticks;
            _kind = kind;
        }
        
        public SafeDateTime(DateTime dateTime)
        {
            _kind = dateTime.Kind;
            _ticks = dateTime.Ticks;
        }
        
        public override string ToString()
        {
            return GetValue().ToString();
        }
    }

    public static class SafeDateTimeExtensions
    {
        public static SafeDateTime ToSafe(this DateTime dateTime)
        {
            return new SafeDateTime(dateTime);
        }
    }
}
