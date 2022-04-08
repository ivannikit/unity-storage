using System.IO;
using TeamZero.StorageSystem.NewtonsoftJson;

#nullable enable

namespace TeamZero.StorageSystem
{
    public class FileSystemSerializeObjectStorage<TData> : SerializeObjectStorage<string, TData>
    {
        private readonly string _directoryPath;
        private readonly string _fileExtension;

        private static IStreamSerializer<object> PlatformFileSystemSerializer(out string fileExtension)
        {
#if UNITY_EDITOR
            fileExtension = ".json";
            return NewtonJsonSerializer.Create();
#elif UNITY_ANDROID
            fileExtension = ".bson";
            return NewtonBsonSerializer.Create();
#elif UNITY_IOS
            fileExtension = ".bson";
            return NewtonBsonSerializer.Create();
#else
            #error This platform are not implemented.
#endif
        }

        public static FileSystemSerializeObjectStorage<TData> Create()
        {
            string directoryPath = FileSystemUtils.GetInternalStoragePath();
            IStreamDatabase<string, object> database = SafeStreamFileSystemDatabase.Create();
            IStreamSerializer<object> serializer = PlatformFileSystemSerializer(out string fileExtension);
            return new FileSystemSerializeObjectStorage<TData>(directoryPath, fileExtension, database, serializer);
        }
        
        private FileSystemSerializeObjectStorage(string directoryPath, string fileExtension,
            IStreamDatabase<string, object> database, 
            IStreamSerializer<object> serializer) : base(database, serializer)
        {
            _directoryPath = directoryPath;
            _fileExtension = fileExtension;
        }

        public override IResource<TData> CreateResource(string address)
        {
            string fileAddress = Path.Combine(_directoryPath, address) + _fileExtension;
            return SerializeObjectResource<string, TData>.Create(fileAddress, _database, _serializer);
        }
    }
}
