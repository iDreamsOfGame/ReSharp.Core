// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="ushort"/>.
    /// </summary>
    public static class UInt16Extensions
    {
        #region Methods

        /// <summary>
        /// Reverse and advances the position of the source by two bytes.
        /// </summary>
        /// <param name="source">The <see cref="ushort"/> to reverse.</param>
        /// <returns>A 2-byte unsigned short integer in reverse.</returns>
        public static ushort Reverse(this ushort source)
        {
            return (ushort)((source & 0xFFU) << 8 | (source & 0xFF00U) >> 8);
        }

        #endregion Methods
    }
}