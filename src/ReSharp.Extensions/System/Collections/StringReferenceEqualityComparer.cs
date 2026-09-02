// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ReSharp.Extensions
{
    /// <summary>
    /// A string comparer that compares strings by reference.
    /// </summary>
    public class StringReferenceEqualityComparer : IEqualityComparer<string>
    {
        /// <summary>
        /// The singleton instance of the <see cref="StringReferenceEqualityComparer"/> class.
        /// </summary>
        public static StringReferenceEqualityComparer Instance { get; private set; } = new StringReferenceEqualityComparer();
        
        private StringReferenceEqualityComparer()
        {
        }

        /// <summary>
        /// Compares two strings for reference equality.
        /// </summary>
        /// <param name="x">The first string to compare.</param>
        /// <param name="y">The second string to compare.</param>
        /// <returns><see langword="true"/> if the strings are equal; otherwise, <see langword="false"/>.</returns>
        public bool Equals(string x, string y) => ReferenceEquals(x, y);

        /// <summary>
        /// Gets the hash code of a string.
        /// </summary>
        /// <param name="obj">The string to get the hash code for.</param>
        /// <returns>The hash code of the string.</returns>
        public int GetHashCode(string obj) => RuntimeHelpers.GetHashCode(obj);
    }
}