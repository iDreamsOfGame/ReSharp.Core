// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

// ReSharper disable InconsistentNaming
namespace System.Collections.Generic
{
    /// <summary>
    /// Extension methods for interface <see cref="IList{T}"/>.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Implements Fisher-Yates shuffle algorithm.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> object. </param>
        /// <param name="seed">The random seed. </param>
        /// <typeparam name="T">The type of list item. </typeparam>
        public static void FisherYatesShuffle<T>(this IList<T> list, int seed = 0)
        {
            var random = seed == 0 ? new Random() : new Random(seed);
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var index = random.Next(n + 1);
                list[index] = list[n];
                list[n] = list[index];
            }
        }
        
        /// <summary>
        /// Sort list items randomly.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> object. </param>
        /// <param name="seed">The random seed. </param>
        /// <typeparam name="T">The type of list item. </typeparam>
        /// <returns>The new see cref="IList{T}"/> object that already randomly sorted. </returns>
        public static IList<T> RandomSort<T>(this IList<T> list, int seed = 0)
        {
            var random = seed == 0 ? new Random() : new Random(seed);
            var newList = new List<T>();
            foreach (var item in list) {
                newList.Insert(random.Next(newList.Count), item);
            }
            
            return newList;
        }
    }
}