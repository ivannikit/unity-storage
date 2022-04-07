#nullable enable
using System;
using System.IO;

namespace TeamZero.StorageSystem
{
    internal static class SafeFileSystemUtils
    {
        internal static string BackupAddress(string address) => ReplicaAddress(address, "backup");
        internal static string TempAddress(string address) => ReplicaAddress(address, "temp");
        private static string ReplicaAddress(string address, string suffix)
        {
            if (string.IsNullOrEmpty(suffix))
                return address;

            string directoryName = Path.GetDirectoryName(address) ?? String.Empty;
            string fileName = Path.GetFileNameWithoutExtension(address);
            string path = string.IsNullOrEmpty(directoryName) ? fileName : Path.Combine(directoryName, fileName);
            
            string fileExtension = Path.GetExtension(address);
            return $"{path}_{suffix}{fileExtension}";
        }
    }
}
