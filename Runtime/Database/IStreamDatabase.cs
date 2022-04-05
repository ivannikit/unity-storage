using System;

#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IStreamDatabase<TData>
    {
        bool Pull(string address, Type valueType, IStreamSerializer<TData> serializer, out TData data);
        bool Push(string address, IStreamSerializer<TData> serializer, TData data);
    }
}
