namespace SharpNEX.Engine.Cache
{
    internal class CacheManager<TKey, TValue>(Func<TKey, TValue> setValue)
        where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _cache = new();
        private readonly object _lock = new();

        public void Clear()
        {
            lock (_lock)
            {
                _cache.Clear();
            }
        }

        public void Remove(TKey key)
        {
            lock (_lock)
            {
                _cache.Remove(key);
            }
        }

        public TValue GetValue(TKey key)
        {
            lock (_lock)
            {
                if (!_cache.TryGetValue(key, out var value))
                {
                    value = setValue(key);
                    if (value is null)
                        throw new ArgumentNullException(nameof(value), "SetValue returned null");

                    _cache[key] = value;
                }
                return value;
            }
        }
    }
}