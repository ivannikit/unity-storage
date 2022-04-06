using System;

#nullable enable

namespace TeamZero.StorageSystem
{
    public static class ResourceFactory
    {
        public static CacheContainer<TData> WitchHash<TData>(this IResource<TData> source) where TData : class
            => CacheContainer<TData>.Create(source);

        public static IResource<TData> WithAutoSave<TData>(this CacheContainer<TData> source,
            ApplicationDelegate appDelegate) where TData : class
        {
            appDelegate.Pause += () => source.PushCache();
            return source;
        }
        
        public static IResource<TData> WithAutoSave<TData>(this CacheContainer<TData> source,
            ApplicationDelegate appDelegate, out IDisposable unsubscribe) where TData : class
        {
            var container = AutoSaveContainer<TData>.Create(source, appDelegate);
            unsubscribe = container;
            return container;
        }
    }
}