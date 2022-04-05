#nullable enable
using System;
using System.IO;

namespace TeamZero.StorageSystem
{
    internal class FileSystemDatabase : IDatabase<string, byte[]>, IDatabase<string, string>, IStreamDatabase<string, object>
    {
        public bool Pull(string address, out byte[] data)
        {
            try
            {
                data = File.ReadAllBytes(address);
                return true;
            }
            catch
            {
                data = default!;
                return false;
            }
        }
        
        public bool Push(string address, byte[] data)
        {
            try
            {
                File.WriteAllBytes(address, data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Pull(string address, out string data)
        {
            try
            {
                data = File.ReadAllText(address);
                return true;
            }
            catch
            {
                data = default!;
                return false;
            }
        }

        public bool Push(string address, string data)
        {
            try
            {
                File.WriteAllText(address, data);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Pull(string address, Type valueType, IStreamSerializer<object> serializer, out object data)
        {
            if (CreatePullStream(address, out Stream stream))
                if (serializer.DeserializeFrom(stream, valueType, out data))
                    return true;

            data = default!;
            return false;
        }

        public bool Push(string address, IStreamSerializer<object> serializer, object data)
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