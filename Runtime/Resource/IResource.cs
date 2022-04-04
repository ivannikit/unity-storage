#nullable enable

namespace TeamZero.StorageSystem
{
    public interface IResource<TData>
    {
        bool Pull(out TData data);
        void Push(TData data);
    }
}