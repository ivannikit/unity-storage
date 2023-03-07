# Unity Storage
Save application data system for Unity Engine.

This package support serialization from Newtonsoft Json SDK ([Newtonsoft Json Unity Package](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@2.0/manual/index.html)).

### Sample:
#### ApplicationData.cs
```csharp
    public class ApplicationData
    {
        public int Diamonds() => _diamonds;
        [JsonProperty] private int _diamonds = 0;
    }
```

#### SaveSystem.cs
```csharp
    var storage = FileSystemSerializeObjectStorage<ApplicationData>.Create();
    
    ApplicationDelegate appDelegate = ...;
    IResource<ApplicationData> resource = storage.CreateResource("file_name")
        .WitchHash()
        .WithAutoSave(appDelegate);
        
    ApplicationData data = resource.Pull(defaultValue: new ApplicationData());
```

### Custom sample:
```csharp
    var storage = FileSystemSerializeObjectStorage<Data>.Create();
    IResource<Data> resource = storage.CreateResource("file_name");
    bool loaded = resource.Pull(out Data data);
    if(loaded)
    {
        //use data
    }
    ...
    
    bool saved = resource.Push(data);
```