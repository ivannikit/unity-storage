using System;

#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IStreamDatabase<in TAddress, TData>
    {
        bool Pull(TAddress address, Type valueType, IStreamSerializer<TData> serializer, out TData data);
        bool Push(TAddress address, IStreamSerializer<TData> serializer, TData data);
    }
}
