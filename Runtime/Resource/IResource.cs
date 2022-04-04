#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IResource<TData>
    {
        bool Pull(out TData data);
        bool Push(TData data);
    }
}