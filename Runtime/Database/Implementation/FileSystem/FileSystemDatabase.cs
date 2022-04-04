#nullable enable
using System.IO;

namespace TeamZero.StorageSystem
{
    internal class FileSystemDatabase : IDatabase<string, byte[]>, IDatabase<string, string>, IStreamDatabase<string>
    {
        internal FileSystemDatabase()
        {
        }
        
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

        public bool CreatePullStream(string address, out Stream stream)
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

        public bool CreatePushStream(string address, out Stream stream)
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