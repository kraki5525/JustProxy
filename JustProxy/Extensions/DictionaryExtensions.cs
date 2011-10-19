using System.Collections.Generic;

namespace JustProxy.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue def)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return def;
        }
    }
}