﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods for interface <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    public static class IDictionaryExtensions
    {
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
            if (!source.ContainsKey(key))
            {
                source.Add(key, value);
            }
            else if (canReplace)
            {
                source[key] = value;
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
        public static TKey GetKey<TKey, TValue>(this IDictionary<TKey, TValue> source, TValue value) => 
            source.FirstOrDefault(q => q.Value.Equals(value)).Key;

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
            var newMap = new TResult();
            foreach (var src in new List<IDictionary<TKey, TValue>> { source }.Concat(others))
            {
                foreach (var pair in src)
                {
                    newMap[pair.Key] = pair.Value;
                }
            }

            return newMap;
        }
    }
}