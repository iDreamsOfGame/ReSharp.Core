// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="long"/>.
    /// </summary>
    public static class Int64Extensions
    {
        /// <summary>
        /// Reverse and advances the position of the source by eight bytes.
        /// </summary>
        /// <param name="source">The <see cref="long"/> to reverse.</param>
        /// <returns>A 8-byte signed long integer in reverse.</returns>
        public static long Reverse(this long source)
        {
            var data = BitConverter.GetBytes(source);
            Array.Reverse(data);
            return BitConverter.ToInt64(data, 0);
        }
    }
}