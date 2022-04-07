using System;

#nullable enable

namespace TeamZero.StorageSystem
{
    public static class ResourceExtension
    {
        public static TData Pull<TData>(this IResource<TData> resource, Func<TData> defaultValue)
        {
            if(!resource.Pull(out TData result))
            {
                result = defaultValue.Invoke();
                resource.Push(result); //autosave feature
            }

            return result;
        }
        
        public static TData Pull<TData>(this IResource<TData> resource, TData defaultValue)
        {
            if(!resource.Pull(out TData result))
            {
                result = defaultValue;
                resource.Push(result); //autosave feature
            }

            return result;
        }
    }
}
