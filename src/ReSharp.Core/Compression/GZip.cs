// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ReSharp.Compression
{
    /// <summary>
    /// Provides methods for compressing and decompressing data by using the GZip data format specification.
    /// </summary>
    public static class GZip
    {
        /// <summary>
        /// The default <see cref="System.Text.Encoding"/> instance for <see cref="string"/>.
        /// </summary>
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        
        /// <summary>
        /// Compress binary data into binary data by using the GZip data format specification.
        /// </summary>
        /// <param name="input">The original binary data. </param>
        /// <param name="level">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing data to the stream. </param>
        /// <returns>The compressed binary data. </returns>
        public static byte[] Compress(byte[] input, CompressionLevel level = CompressionLevel.Optimal)
        {
            if (input == null || input.Length == 0)
                return null;
            
            using (var outputStream = new MemoryStream())
            {
                using (var deflateStream = new GZipStream(outputStream, level))
                {
                    deflateStream.Write(input, 0, input.Length);
                    deflateStream.Flush();
                }

                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// Compress <see cref="string"/> which encoded in <c>encoding</c> into binary data by using the GZip data format specification.
        /// </summary>
        /// <param name="source">The original <see cref="string"/>. </param>
        /// <param name="encoding">The <see cref="Encoding"/> instance to get binary data from the original <see cref="string"/>. </param>
        /// <param name="level">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing data to the stream. </param>
        /// <returns>The compressed binary data. </returns>
        public static byte[] Compress(string source, Encoding encoding = null, CompressionLevel level = CompressionLevel.Optimal)
        {
            if (string.IsNullOrEmpty(source))
                return null;
            
            if (encoding == null)
                encoding = DefaultEncoding;

            var input = encoding.GetBytes(source);
            return Compress(input, level);
        }

        /// <summary>
        /// Compress binary data into Base64 <see cref="string"/> by using the GZip data format specification.
        /// </summary>
        /// <param name="input">The original binary data. </param>
        /// <param name="level">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing data to the stream. </param>
        /// <returns>The compressed data which converted to Base64 <see cref="string"/>. </returns>
        public static string CompressToBase64String(byte[] input, CompressionLevel level = CompressionLevel.Optimal)
        {
            var output = Compress(input, level);
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Compress <see cref="string"/> which encoded in <c>encoding</c> into Base64 <see cref="string"/> by using the GZip data format specification.
        /// </summary>
        /// <param name="source">The original <see cref="string"/>. </param>
        /// <param name="encoding">The <see cref="Encoding"/> instance to get binary data from the original <see cref="string"/>. </param>
        /// <param name="level">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing data to the stream. </param>
        /// <returns>The compressed data which converted to Base64 <see cref="string"/>. </returns>
        public static string CompressToBase64String(string source, Encoding encoding = null, CompressionLevel level = CompressionLevel.Optimal)
        {
            var output = Compress(source, encoding, level);
            return Convert.ToBase64String(output);
        }
        
        /// <summary>
        /// Decompress into original binary data from input binary data by using the GZip data format specification.
        /// </summary>
        /// <param name="input">The compressed binary data. </param>
        /// <returns>The original data. </returns>
        public static byte[] Decompress(byte[] input)
        {
            if (input == null || input.Length == 0)
                return null;
            
            using (var inputStream = new MemoryStream(input))
            {
                using (var deflateStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    using (var outputStream = new MemoryStream())
                    {
                        deflateStream.CopyTo(outputStream);
                        return outputStream.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Decompress into <see cref="string"/> which encoded in <c>encoding</c> from input binary data by using the GZip data format specification.
        /// </summary>
        /// <param name="input">The compressed binary data. </param>
        /// <param name="encoding">The <see cref="Encoding"/> instance to get binary data from the original binary data. </param>
        /// <returns>The original <see cref="string"/>. </returns>
        public static string Decompress(byte[] input, Encoding encoding)
        {
            if (encoding == null)
                encoding = DefaultEncoding;
            
            var output = Decompress(input);
            return encoding.GetString(output);
        }

        /// <summary>
        /// Decompress into binary data from Base64 <see cref="string"/> by using the GZip data format specification.
        /// </summary>
        /// <param name="source">The Bass64 <see cref="string"/>. </param>
        /// <returns>The original data. </returns>
        public static byte[] DecompressFromBase64String(string source)
        {
            if (string.IsNullOrEmpty(source))
                return null;
            
            var input = Convert.FromBase64String(source);
            return Decompress(input);
        }

        /// <summary>
        /// Decompress into <see cref="string"/> which encoded in <c>encoding</c> from Base64 <see cref="string"/> by using the GZip data format specification.
        /// </summary>
        /// <param name="source">The Bass64 <see cref="string"/>. </param>
        /// <param name="encoding">The <see cref="Encoding"/> instance to get binary data from the original binary data. </param>
        /// <returns>The original <see cref="string"/>. </returns>
        public static string DecompressFromBase64String(string source, Encoding encoding)
        {
            if (string.IsNullOrEmpty(source))
                return null;

            var input = Convert.FromBase64String(source);
            return Decompress(input, encoding);
        }
    }
}