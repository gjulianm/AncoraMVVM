
namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Get the element for the given key or create it with its default constructor.
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dic">Dictionary</param>
        /// <param name="key">Key to retrieve</param>
        /// <returns>The item retrieved or created</returns>
        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key) where TValue : new()
        {
            TValue value;

            if (!dic.TryGetValue(key, out value))
            {
                value = new TValue();
                dic[key] = value;
            }

            return value;
        }
    }
}
