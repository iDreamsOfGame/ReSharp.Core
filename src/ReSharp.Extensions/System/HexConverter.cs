// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Text;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Provides utility methods for converting between hexadecimal strings and byte arrays.
    /// </summary>
    public static class HexConverter
    {
        /// <summary>
        /// Converts a hexadecimal string to a byte array.
        /// </summary>
        /// <param name="hex">The hexadecimal string to convert.</param>
        /// <param name="separator">The optional separator character between hex values (e.g., ':' or '-'). Use '\0' for no separator.</param>
        /// <returns>A byte array represented by the hexadecimal string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the hex string is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the hex string length is not even after removing separators.</exception>
        /// <example>
        /// <code>
        /// byte[] bytes = HexConverter.FromHexString("48656C6C6F");
        /// byte[] bytesWithSeparator = HexConverter.FromHexString("48:65:6C:6C:6F", ':');
        /// </code>
        /// </example>
        public static byte[] FromHexString(string hex, char separator = '\0')
        {
            if (string.IsNullOrEmpty(hex))
                throw new ArgumentNullException(nameof(hex));

            // Remove all non-hexadecimal characters (including delimiters)
            hex = hex.Replace(separator.ToString(), "")
                .Replace("0x", "")
                .Replace("0X", "");

            if (hex.Length % 2 != 0)
                throw new ArgumentException("Hex string length must be even. ");

            var byteLength = hex.Length / 2;
            var bytes = new byte[byteLength];

            for (var i = 0; i < byteLength; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        /// <summary>
        /// Converts a byte array to a hexadecimal string.
        /// </summary>
        /// <param name="bytes">The byte array to convert.</param>
        /// <param name="format">The format specifier for each byte. Default is "X2" (uppercase hexadecimal with leading zeros).</param>
        /// <param name="separator">The optional separator character to insert between hex values. Use '\0' for no separator.</param>
        /// <returns>A hexadecimal string representation of the byte array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the byte array is null.</exception>
        /// <example>
        /// <code>
        /// string hex = HexConverter.ToHexString(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F });
        /// string hexWithSeparator = HexConverter.ToHexString(new byte[] { 0x48, 0x65 }, ':');
        /// </code>
        /// </example>
        public static string ToHexString(byte[] bytes, string format = "X2", char separator = '\0')
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var stringBuilder = new StringBuilder(bytes.Length * (2 + (separator != '\0' ? 1 : 0)));
            for (var i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString(format));
                if (separator != '\0' && i < bytes.Length - 1)
                    stringBuilder.Append(separator);
            }

            return stringBuilder.ToString();
        }
    }
}