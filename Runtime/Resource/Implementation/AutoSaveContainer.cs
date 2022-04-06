using System;

#nullable enable

namespace TeamZero.StorageSystem
{
    public class AutoSaveContainer<TData> : IResource<TData>, IDisposable where TData : class
    {
        private readonly CacheContainer<TData> _source;
        private readonly ApplicationDelegate _appDelegate;

        public static AutoSaveContainer<TData> Create(CacheContainer<TData> source, ApplicationDelegate appDelegate) 
            => new AutoSaveContainer<TData>(source, appDelegate);
        
        private AutoSaveContainer(CacheContainer<TData> source, ApplicationDelegate appDelegate)
        {
            _source = source;
            _appDelegate = appDelegate;
            _appDelegate.Pause += OnApplicationPause;
        }

        public bool Pull(out TData data) => _source.Pull(out data);

        public bool Push(TData data) => _source.Push(data);

        private void OnApplicationPause() => _source.PushCache();
        
        public void Dispose()
        {
            if(_appDelegate is { })
                _appDelegate.Pause -= OnApplicationPause;
        }
    }
}