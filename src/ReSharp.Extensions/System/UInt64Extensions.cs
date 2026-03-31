// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="ulong"/>.
    /// </summary>
    public static class UInt64Extensions
    {
        /// <summary>
        /// Reverse and advances the position of the source by eight bytes.
        /// </summary>
        /// <param name="source">The <see cref="ulong"/> to reverse.</param>
        /// <returns>A 8-byte unsigned long integer in reverse.</returns>
        public static ulong Reverse(this ulong source) =>
            (source & 0x00000000000000FFUL) << 56 |
            (source & 0x000000000000FF00UL) << 40 |
            (source & 0x0000000000FF0000UL) << 24 |
            (source & 0x00000000FF000000UL) << 8 |
            (source & 0x000000FF00000000UL) >> 8 |
            (source & 0x0000FF0000000000UL) >> 24 |
            (source & 0x00FF000000000000UL) >> 40 |
            (source & 0xFF00000000000000UL) >> 56;
        
        /// <summary>
        /// Gets the integer number that the last <c>digits</c> present.
        /// </summary>
        /// <param name="source">The <see cref="ulong"/> to convert.</param>
        /// <param name="digits">The digits. </param>
        /// <returns></returns>
        public static int GetLastDigits(this ulong source, int digits = 1)
        {
            if (digits <= 0)
                throw new ArgumentException("digits must be greater than zero!");
                
            return (int)source % (int)Math.Pow(10, digits);
        }
    }
}