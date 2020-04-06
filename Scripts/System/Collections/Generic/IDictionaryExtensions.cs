// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// Extension methods for interface <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class IDictionaryExtensions
    {
        #region Methods

        /// <summary>
        /// Adds the value with unique key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source Dictionary object.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="canReplace">
        /// if set to <c>true</c>, the value will be replaced when find same key.
        /// </param>
        public static void AddUnique<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value, bool canReplace = true)
        {
            try
            {
                source.Add(key, value);
            }
            catch (ArgumentException)
            {
                if (canReplace)
                {
                    source[key] = value;
                }
            }
        }

        /// <summary>
        /// Gets the key by value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source Dictionary object.</param>
        /// <param name="value">The value.</param>
        /// <returns>The value object.</returns>
        public static TKey GetKey<TKey, TValue>(this IDictionary<TKey, TValue> source, TValue value)
        {
            return source.FirstOrDefault(q => q.Value.Equals(value)).Key;
        }

        /// <summary>
        /// Merges dictionaries.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <param name="others">The other dictionaries.</param>
        /// <returns>The dictionary contains all values of source dictionary and others.</returns>
        public static TResult MergeLeft<TResult, TKey, TValue>(this TResult source, params IDictionary<TKey, TValue>[] others)
            where TResult : IDictionary<TKey, TValue>, new()
        {
            TResult newMap = new TResult();
            foreach (IDictionary<TKey, TValue> src in
                (new List<IDictionary<TKey, TValue>> { source }).Concat(others))
            {
                foreach (KeyValuePair<TKey, TValue> p in src)
                {
                    newMap[p.Key] = p.Value;
                }
            }
            return newMap;
        }

        #endregion Methods
    }
}