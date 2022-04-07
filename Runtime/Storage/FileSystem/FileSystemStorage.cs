using Newtonsoft.Json;
using TeamZero.StorageSystem.NewtonsoftJson;

#nullable enable

namespace TeamZero.StorageSystem
{
    public class JsonFileSystemStorage<TData> : StreamStorage<string, TData>
    {
        private readonly string _directoryPath;

        
        
        public static JsonFileSystemStorage<TData> Create()
        {
            string directoryPath = FileSystemUtils.GetInternalStoragePath();
            IStreamDatabase<string, TData> database = SafeFileSystemDatabase<TData>.Create();
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            IStreamSerializer<TData> serializer = NewtonJsonSerializer<TData>.Create(serializerSettings);
            return new JsonFileSystemStorage<TData>(directoryPath, database, serializer);
        }
        
        private JsonFileSystemStorage(string directoryPath, 
            IStreamDatabase<string, TData> database, 
            IStreamSerializer<TData> serializer) : base(database, serializer)
        {
            _directoryPath = directoryPath;
        }

        public override IResource<TData> CreateResource(string address)
            => StreamResource<string, TData>.Create(address, _database, _serializer);
    }
}
