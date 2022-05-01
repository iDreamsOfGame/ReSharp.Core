// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="int"/>.
    /// </summary>
    public static class Int32Extensions
    {
        #region Methods

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

        #endregion Methods
    }
}