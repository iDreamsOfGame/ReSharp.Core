// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.IO;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="BinaryReader"/>.
    /// </summary>
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads the specified number of bytes in reverse from the current stream into a byte array
        /// and advances the current position by that number of bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read byte array.</param>
        /// <param name="count">
        /// The number of bytes to read. This value must be 0 or a non-negative number or an
        /// exception will occur.
        /// </param>
        /// <returns>
        /// A byte array containing data in reverse read from the underlying stream. This might be
        /// less than the number of bytes requested if the end of the stream is reached.
        /// </returns>
        public static byte[] ReadBytesReverse(this BinaryReader reader, int count)
        {
            var data = reader.ReadBytes(count);
            Array.Reverse(data);
            return data;
        }

        /// <summary>
        /// Reads a 2-byte signed integer in reverse from the current stream and advances the
        /// current position of the stream by two bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read signed short integer.</param>
        /// <returns>A 2-byte signed short integer in reverse read from the current stream.</returns>
        public static short ReadInt16Reverse(this BinaryReader reader)
        {
            var value = reader.ReadInt16();
            return value.Reverse();
        }

        /// <summary>
        /// Reads a 4-byte signed integer in reverse from the current stream and advances the
        /// current position of the stream by four bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read signed integer.</param>
        /// <returns>A 4-byte signed integer in reverse read from the current stream.</returns>
        public static int ReadInt32Reverse(this BinaryReader reader)
        {
            var value = reader.ReadInt32();
            return value.Reverse();
        }

        /// <summary>
        /// Reads an 8-byte signed integer in reverse from the current stream and advances the
        /// current position of the stream by eight bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read signed long integer.</param>
        /// <returns>A 8-byte signed long integer in reverse read from the current stream.</returns>
        public static long ReadInt64Reverse(this BinaryReader reader)
        {
            var value = reader.ReadInt64();
            return value.Reverse();
        }

        /// <summary>
        /// Reads a 2-byte unsigned integer in reverse from the current stream and advances the
        /// position of the stream by two bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read unsigned short integer.</param>
        /// <returns>A 2-byte unsigned short integer in reverse read from this stream.</returns>
        public static ushort ReadUInt16Reverse(this BinaryReader reader)
        {
            var value = reader.ReadUInt16();
            return value.Reverse();
        }

        /// <summary>
        /// Reads a 4-byte unsigned integer in reverse from the current stream and advances the
        /// position of the stream by four bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read unsigned integer.</param>
        /// <returns>A 4-byte unsigned integer in reverse read from this stream.</returns>
        public static uint ReadUInt32Reverse(this BinaryReader reader)
        {
            var value = reader.ReadUInt32();
            return value.Reverse();
        }

        /// <summary>
        /// Reads an 8-byte unsigned integer in reverse from the current stream and advances the
        /// position of the stream by eight bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read unsigned long integer.</param>
        /// <returns>A 8-byte unsigned long integer in reverse read from this stream.</returns>
        public static ulong ReadUInt64Reverse(this BinaryReader reader)
        {
            var value = reader.ReadUInt64();
            return value.Reverse();
        }
    }
}