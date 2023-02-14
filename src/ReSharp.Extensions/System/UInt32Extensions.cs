﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="uint"/>.
    /// </summary>
    public static class UInt32Extensions
    {
        /// <summary>
        /// Reverse and advances the position of the source by four bytes.
        /// </summary>
        /// <param name="source">The <see cref="uint"/> to reverse.</param>
        /// <returns>A 4-byte unsigned integer in reverse.</returns>
        public static uint Reverse(this uint source) =>
            (source & 0x000000FFU) << 24 |
            (source & 0x0000FF00U) << 8 |
            (source & 0x00FF0000U) >> 8 |
            (source & 0xFF000000U) >> 24;
        
        /// <summary>
        /// Gets the integer number that the last <c>digits</c> present.
        /// </summary>
        /// <param name="source">The <see cref="uint"/> to convert.</param>
        /// <param name="digits">The digits. </param>
        /// <returns></returns>
        public static int GetLastDigits(this uint source, int digits = 1)
        {
            if (digits <= 0)
                throw new ArgumentException("digits must be greater than zero!");
                
            return (int)source % (int)Math.Pow(10, digits);
        }
    }
}