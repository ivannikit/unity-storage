# Unity Storage
Save application data system for Unity Engine.

This package support serialization from Newtonsoft Json SDK ([Newtonsoft Json Unity Package](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@2.0/manual/index.html)).

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
    var storage = FileSystemSerializeObjectStorage<TData>.Create();
    
    ApplicationDelegate appDelegate = ...;
    IResource<ApplicationData> appData = storage.CreateResource("file_name")
        .WitchHash()
        .WithAutoSave(appDelegate);
        
    IResource<CustomData> customResource = storage.CreateResource("custom_file_name");
    bool loaded = customResource.Pull(out CustomData customData);
    if(loaded)
    {
        //use customData
    }
    ...
    
    bool saved = customResource.Push(customData);
```
