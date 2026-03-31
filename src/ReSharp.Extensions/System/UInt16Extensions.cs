// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="ushort"/>.
    /// </summary>
    public static class UInt16Extensions
    {
        /// <summary>
        /// Reverse and advances the position of the source by two bytes.
        /// </summary>
        /// <param name="source">The <see cref="ushort"/> to reverse.</param>
        /// <returns>A 2-byte unsigned short integer in reverse.</returns>
        public static ushort Reverse(this ushort source) => (ushort)((source & 0xFFU) << 8 | (source & 0xFF00U) >> 8);
        
        /// <summary>
        /// Gets the integer number that the last <c>digits</c> present.
        /// </summary>
        /// <param name="source">The <see cref="ushort"/> to convert.</param>
        /// <param name="digits">The digits. </param>
        /// <returns></returns>
        public static int GetLastDigits(this ushort source, int digits = 1)
        {
            if (digits <= 0)
                throw new ArgumentException("digits must be greater than zero!");
                
            return source % (int)Math.Pow(10, digits);
        }
    }
}