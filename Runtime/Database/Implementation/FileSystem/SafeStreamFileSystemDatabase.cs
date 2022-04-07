#nullable enable
using System;
using System.IO;

namespace TeamZero.StorageSystem
{
    public class SafeStreamFileSystemDatabase<TData> : IStreamDatabase<string, TData>
    {
        private readonly StreamFileSystemDatabase<TData> _database;

        public static SafeStreamFileSystemDatabase<TData> Create()
        {
            StreamFileSystemDatabase<TData> database = new StreamFileSystemDatabase<TData>();
            return new SafeStreamFileSystemDatabase<TData>(database);
        }

        private SafeStreamFileSystemDatabase(StreamFileSystemDatabase<TData> database) => _database = database;
        
        public bool Pull(string address, Type valueType, IStreamSerializer<TData> serializer, out TData data)
        {
            if (_database.Pull(address, valueType, serializer, out data))
                    return true;

            string backupAddress = SafeFileSystemUtils.BackupAddress(address);
            return _database.Pull(backupAddress, valueType, serializer, out data);
        }

        public bool Push(string address, IStreamSerializer<TData> serializer, TData data)
        {
            string tempAddress = SafeFileSystemUtils.TempAddress(address);
            if (_database.Push(tempAddress, serializer, data))
            {
                SeekReplicaFiles(address, tempAddress);
                return true;
            }

            return false;
        }

        private void SeekReplicaFiles(string currentAddress, string tempAddress)
        {
            string backupPatch = SafeFileSystemUtils.BackupAddress(currentAddress);
            File.Delete(backupPatch);
            if (File.Exists(currentAddress))
                File.Move(currentAddress, backupPatch);
            File.Move(tempAddress, currentAddress);
        }
    }
}