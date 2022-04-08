#nullable enable
using System.IO;

namespace TeamZero.StorageSystem
{
    public class SafeFileSystemDatabase : IDatabase<string, byte[]>, IDatabase<string, string>
    {
        private readonly FileSystemDatabase _database;

        public static SafeFileSystemDatabase Create()
        {
            FileSystemDatabase database = new FileSystemDatabase();
            return new SafeFileSystemDatabase(database);
        }

        private SafeFileSystemDatabase(FileSystemDatabase database) => _database = database;

        public bool Pull(string address, out byte[] data)
        {
            if (_database.Pull(address, out data))
                return true;

            string backupPatch = SafeFileSystemUtils.BackupAddress(address);
            return _database.Pull(backupPatch, out data);
        }

        public bool Push(string address, byte[] data)
        { 
            string tempAddress = SafeFileSystemUtils.TempAddress(address);
            if (_database.Push(tempAddress, data))
            {
                SeekReplicaFiles(address, tempAddress);
                return true;
            }

            return false;
        }

        public bool Pull(string address, out string data)
        {
            if (_database.Pull(address, out data))
                return true;

            string backupPatch = SafeFileSystemUtils.BackupAddress(address);
            return _database.Pull(backupPatch, out data);
        }

        public bool Push(string address, string data)
        { 
            string tempAddress = SafeFileSystemUtils.TempAddress(address);
            if (_database.Push(tempAddress, data))
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