using System;
using System.Collections.Generic;

namespace SharpNEX.Engine.Components
{
    internal class CacheManager<Key, Value>
    {
        private readonly Dictionary<Key, Value> cache;
        private readonly Func<Key, Value> SetValue;

        public CacheManager(Func<Key, Value> setValue)
        {
            cache = new Dictionary<Key, Value>();
            SetValue = setValue;
        }

        public Value GetValue(Key key)
        {
            bool exits = cache.TryGetValue(key, out var values);

            if (!exits)
            {
                values = SetValue(key);
                cache[key] = values;
            }

            return values;
        }
    }
}
