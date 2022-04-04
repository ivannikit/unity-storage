using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable

namespace TeamZero.StorageSystem
{
    public interface ITypedValueSerializer<TValue, TSerializedValue>
    {
        bool Deserialize(Type valueType, TSerializedValue serializedValue, [NotNullWhen(true)] out TValue value);
        bool Serialize(TValue value, [NotNullWhen(true)] out TSerializedValue serializedValue);
    }
}
