using System.IO;
using TeamZero.StorageSystem.NewtonsoftJson;

#nullable enable

namespace TeamZero.StorageSystem
{
    public class FileSystemStorage<TData> : StreamStorage<string, TData>
    {
        private readonly string _directoryPath;
        private readonly string _fileExtension;

        private static IStreamSerializer<TData> PlatformFileSystemSerializer(out string fileExtension)
        {
#if UNITY_EDITOR
            fileExtension = ".json";
            return NewtonJsonSerializer<TData>.Create();
#elif UNITY_ANDROID
            fileExtension = ".bson";
            return NewtonBsonSerializer<TData>.Create();
#elif UNITY_IOS
            fileExtension = ".bson";
            return NewtonBsonSerializer<TData>.Create();
#else
            #error This platform are not implemented.
#endif
        }

        public static FileSystemStorage<TData> Create()
        {
            string directoryPath = FileSystemUtils.GetInternalStoragePath();
            IStreamDatabase<string, TData> database = SafeStreamFileSystemDatabase<TData>.Create();
            IStreamSerializer<TData> serializer = PlatformFileSystemSerializer(out string fileExtension);
            return new FileSystemStorage<TData>(directoryPath, fileExtension, database, serializer);
        }
        
        private FileSystemStorage(string directoryPath, string fileExtension,
            IStreamDatabase<string, TData> database, 
            IStreamSerializer<TData> serializer) : base(database, serializer)
        {
            _directoryPath = directoryPath;
            _fileExtension = fileExtension;
        }

        public override IResource<TData> CreateResource(string address)
        {
            string fileAddress = Path.Combine(_directoryPath, address) + _fileExtension;
            return StreamResource<string, TData>.Create(fileAddress, _database, _serializer);
        }
    }
}
