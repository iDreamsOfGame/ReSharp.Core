// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace System.Collections.Generic
{
    /// <summary>
    /// Extension methods for generic collection classes.
    /// </summary>
	public static class GenericCollectionExtensions
    {
        #region Methods

        /// <summary>
        /// Adds a unique item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="source">A <see cref="ICollection{T}"/> to add unique item.</param>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
        public static void AddUnique<T>(this ICollection<T> source, T item)
        {
            if (source.Contains(item))
                return;
            
            source.Add(item);
        }

        #endregion Methods
    }
}