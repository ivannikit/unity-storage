#nullable enable

namespace TeamZero.StorageSystem
{
    public interface ISerializer<TValue, TSerializedValue>
    {
        bool Deserialize(TSerializedValue serializedValue, out TValue value);
        bool Serialize(TValue value, out TSerializedValue serializedValue);
    }
}
