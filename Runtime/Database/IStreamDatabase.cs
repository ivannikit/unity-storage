using System;

#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IStreamDatabase<in TAddress, TData>
    {
        bool Pull(TAddress address, Type valueType, IStreamSerializer<object> serializer, out TData data);
        bool Push(TAddress address, IStreamSerializer<object> serializer, TData data);
    }
}
