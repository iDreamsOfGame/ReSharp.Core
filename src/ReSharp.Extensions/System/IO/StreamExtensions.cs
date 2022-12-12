// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.IO;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Convert a <see cref="Stream"/> to a byte array.
        /// </summary>
        /// <param name="input">The input <see cref="Stream"/>.</param>
        /// <returns>The byte array converted.</returns>
        public static byte[] ToByteArray(this Stream input)
        {
            var buffer = new byte[16 * 1024];

            using (var stream = new MemoryStream())
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, read);
                }

                return stream.ToArray();
            }
        }
    }
}