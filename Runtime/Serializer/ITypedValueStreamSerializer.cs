#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace TeamZero.StorageSystem
{
    public interface ITypedValueStreamSerializer<TValue>
    {
        bool DeserializeFrom(Stream stream, Type valueType, [NotNullWhen(true)] out TValue value);
        bool SerializeTo(Stream stream, TValue value);
    }
}
