// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="int"/>.
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// Reverse and advances the position of the source by four bytes.
        /// </summary>
        /// <param name="source">The <see cref="int"/> to reverse.</param>
        /// <returns>A 4-byte signed integer in reverse.</returns>
        public static int Reverse(this int source)
        {
            var data = BitConverter.GetBytes(source);
            Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Gets the integer number that the last <c>digits</c> present.
        /// </summary>
        /// <param name="source">The <see cref="int"/> to convert.</param>
        /// <param name="digits">The digits. </param>
        /// <returns></returns>
        public static int GetLastDigits(this int source, int digits = 1)
        {
            if (digits <= 0)
                throw new ArgumentException("digits must be greater than zero!");
                
            return Math.Abs(source) % (int)Math.Pow(10, digits);
        }
    }
}