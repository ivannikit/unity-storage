#nullable enable
using System;
using System.IO;

namespace TeamZero.StorageSystem
{
    /// <summary>
    /// This class are internal because it's not safe. Use SafeFileSystemDatabase instead
    /// </summary>
    internal class StreamFileSystemDatabase<TData> : IStreamDatabase<string, TData>
    {
        public bool Pull(string address, Type valueType, IStreamSerializer<TData> serializer, out TData data)
        {
            if (CreatePullStream(address, out Stream stream))
                if (serializer.DeserializeFrom(stream, valueType, out data))
                    return true;

            data = default!;
            return false;
        }

        public bool Push(string address, IStreamSerializer<TData> serializer, TData data)
        {
            if (CreatePushStream(address, out Stream stream))
                return serializer.SerializeTo(stream, data);

            return false;
        }
        
        private bool CreatePullStream(string address, out Stream stream)
        {
            try
            {
                stream = File.OpenRead(address);
                return true;
            }
            catch
            {
                stream = default!;
                return false;
            }
        }

        private bool CreatePushStream(string address, out Stream stream)
        {
            try
            {
                stream = File.Create(address);
                return true;
            }
            catch
            {
                stream = default!;
                return false;
            }
        }
    }
}