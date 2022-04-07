#nullable enable

namespace TeamZero.StorageSystem
{
    public class CacheContainer<TData> : IResource<TData>
    {
        private readonly IResource<TData> _source;
        private TData _cache = default!;
        private bool _cached = false;

        public static CacheContainer<TData> Create(IResource<TData> source) 
            => new CacheContainer<TData>(source);
        
        private CacheContainer(IResource<TData> source) => _source = source;

        public bool Pull(out TData data)
        {
            if (!_cached)
            {
                if (_source.Pull(out TData sourceData))
                {
                    _cache = sourceData;
                    _cached = true;
                }
            }

            data = _cache;
            return _cached;
        }

        public bool Push(TData data)
        {
            _cache = data;
            _cached = true;
            return _source.Push(data);
        }

        public bool PushCache()
        {
            bool result = false;
            if(_cached)
                result = _source.Push(_cache);

            return result;
        }
    }
}