// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace System.IO
{
    /// <summary>
    /// Extension methods collection of <see cref="Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        #region Methods

        /// <summary>
        /// Convert a <see cref="Stream"/> to a byte array.
        /// </summary>
        /// <param name="input">The input <see cref="Stream"/>.</param>
        /// <returns>The byte array converted.</returns>
        public static byte[] ToByteArray(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];

            using (var ms = new MemoryStream())
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        #endregion Methods
    }
}