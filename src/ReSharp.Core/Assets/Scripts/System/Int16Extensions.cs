﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="short"/>.
    /// </summary>
    public static class Int16Extensions
    {
        #region Methods

        /// <summary>
        /// Reverse and advances the position of the source by two bytes.
        /// </summary>
        /// <param name="source">The <see cref="short"/> to reverse.</param>
        /// <returns>A 2-byte signed short integer in reverse.</returns>
        public static short Reverse(this short source)
        {
            byte[] data = BitConverter.GetBytes(source);
            Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }

        #endregion Methods
    }
}