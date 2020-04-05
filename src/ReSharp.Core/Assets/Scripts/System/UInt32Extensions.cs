// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="uint"/>.
    /// </summary>
    public static class UInt32Extensions
    {
        #region Methods

        /// <summary>
        /// Reverse and advances the position of the source by four bytes.
        /// </summary>
        /// <param name="source">The <see cref="uint"/> to reverse.</param>
        /// <returns>A 4-byte unsigned integer in reverse.</returns>
        public static uint Reverse(this uint source)
        {
            return (source & 0x000000FFU) << 24 |
                (source & 0x0000FF00U) << 8 |
                (source & 0x00FF0000U) >> 8 |
                (source & 0xFF000000U) >> 24;
        }

        #endregion Methods
    }
}