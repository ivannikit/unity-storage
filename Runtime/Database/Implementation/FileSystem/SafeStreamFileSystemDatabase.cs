#nullable enable
using System;
using System.IO;

namespace TeamZero.StorageSystem
{
    public class SafeStreamFileSystemDatabase : IStreamDatabase<string, object>
    {
        private readonly StreamFileSystemDatabase _database;

        public static SafeStreamFileSystemDatabase Create()
        {
            StreamFileSystemDatabase database = new StreamFileSystemDatabase();
            return new SafeStreamFileSystemDatabase(database);
        }

        private SafeStreamFileSystemDatabase(StreamFileSystemDatabase database) => _database = database;
        
        public bool Pull(string address, Type valueType, IStreamSerializer<object> serializer, out object data)
        {
            if (_database.Pull(address, valueType, serializer, out data))
                    return true;

            string backupAddress = SafeFileSystemUtils.BackupAddress(address);
            return _database.Pull(backupAddress, valueType, serializer, out data);
        }

        public bool Push(string address, IStreamSerializer<object> serializer, object data)
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