// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace System.Collections
{
    /// <summary>
    /// Extension methods for collection classes.
    /// </summary>
	public static class CollectionExtensions
    {
        #region Methods

        /// <summary>
        /// Adds a unique item to the <see cref="IList"/>.
        /// </summary>
        /// <param name="source">A <see cref="IList"/> to add unique item.</param>
        /// <param name="value">The object to add to the <see cref="IList"/>.</param>
        /// <returns>
        /// The position into which the new element was inserted, or -1 to indicate that the item
        /// was not inserted into the collection.
        /// </returns>
        public static int AddUnique(this IList source, object value)
        {
            var position = -1;

            if (!source.Contains(value))
            {
                position = source.Add(value);
            }

            return position;
        }

        /// <summary>
        /// Copies all the elements of the <see cref="IList"/> to the specific <see cref="IList"/>.
        /// </summary>
        /// <param name="source">The source object of the <see cref="IList"/>.</param>
        /// <param name="target">The target object of the <see cref="IList"/>.</param>
        /// <param name="index">
        /// A 32-bit integer that represents the index in <see cref="IList"/> at which copying begins.
        /// </param>
        public static void CopyTo(this IList source, IList target, int index = 0)
        {
            var sourceLength = source.Count;
            var targetLength = target.Count;

            index = Math.Max(Math.Min(sourceLength - 1, index), 0);

            for (var i = index; i < sourceLength; ++i)
            {
                var j = i - index;

                if (j < targetLength)
                {
                    target[j] = source[i];
                }
            }
        }

        /// <summary>
        /// Searches for the maximum object and returns the index of object in the <see cref="IList"/>.
        /// </summary>
        /// <param name="source">The <see cref="IList"/> to search.</param>
        /// <param name="count">The number of objects in the section to search.</param>
        /// <returns>The index of maximum object in the <see cref="IList"/>.</returns>
        public static int IndexOfMax(this IList source, int count)
        {
            var index = -1;
            var length = source.Count;

            count = Math.Max(Math.Min(count, source.Count), 0);

            if (length <= 1) 
                return index;
            
            index = 0;
            for (var i = 0; i < count; ++i)
            {
                if (((IComparable)source[i]).CompareTo(source[index]) > 0)
                {
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// Swaps a element in one index with another element in another index.
        /// </summary>
        /// <param name="source">The <see cref="IList"/> to swap elements.</param>
        /// <param name="a">The first index of element in the <see cref="IList"/> to swap.</param>
        /// <param name="b">The second index of element in the <see cref="IList"/> to swap.</param>
        public static void Swap(this IList source, int a, int b)
        {
            if (a < 0 || a >= source.Count || b < 0 || b >= source.Count) 
                return;
            
            var temp = source[b];
            source[b] = source[a];
            source[a] = temp;
        }

        /// <summary>
        /// Converts the value of the current <see cref="IList"/> to its equivalent array string representation.
        /// </summary>
        /// <param name="source">The source <see cref="IList"/> object.</param>
        /// <returns>The array string representation of the value of <see cref="IList"/>.</returns>
        public static string ToArrayString(this IList source)
        {
            var stringCollection = new string[source.Count];

            for (int i = 0, length = source.Count; i < length; ++i)
            {
                stringCollection[i] = source[i].ToString();
            }

            return $"{{ {string.Join(", ", stringCollection)} }}";
        }

        #endregion Methods
    }
}