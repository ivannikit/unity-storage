using System;
using System.IO;

#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IStreamSerializer<TData>
    {
        bool DeserializeFrom(Stream stream, Type valueType, out TData value);
        bool SerializeTo(Stream stream, TData value);
    }
}
