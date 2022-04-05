#nullable enable
using System.IO;

namespace TeamZero.StorageSystem
{
    internal class FileSystemDatabase : IDatabase<string, byte[]>, IDatabase<string, string>
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
    }
}