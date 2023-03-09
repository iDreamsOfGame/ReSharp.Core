// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Security.Cryptography;
using System.Text;

// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable ConvertToUsingDeclaration

namespace ReSharp.Security.Cryptography
{
    /// <summary>
    /// Provides cryptographic service.
    /// </summary>
    public static class CryptoUtility
    {
        private const string LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";

        private const string Numbers = "0123456789";

        private const string SpecialCharacters = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        private const string UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Default coding for encryption or decryption. 
        /// </summary>
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// Decrypts the cipher text by using the AES encryption algorithm. 
        /// </summary>
        /// <param name="cipherText">The raw data of cipher text. </param>
        /// <param name="key">The raw data of key. </param>
        /// <returns>The raw data of plain text. </returns>
        /// <exception cref="ArgumentNullException">cipherText or key</exception>
        public static byte[] AesDecrypt(byte[] cipherText, byte[] key)
        {
            if (cipherText == null || cipherText.Length == 0)
                throw new ArgumentNullException(nameof(cipherText));

            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));

            using (var rm = new RijndaelManaged { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                using (var transform = rm.CreateDecryptor())
                {
                    return transform.TransformFinalBlock(cipherText, 0, cipherText.Length);
                }
            }
        }

        /// <summary>
        /// Decrypts the cipher text by using the AES encryption algorithm. 
        /// </summary>
        /// <param name="cipherText">The cipher text. </param>
        /// <param name="key">The key. </param>
        /// <returns>The plain text. </returns>
        /// <exception cref="ArgumentNullException">cipherText or key</exception>
        public static string AesDecrypt(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return DefaultEncoding.GetString(AesDecrypt(DefaultEncoding.GetBytes(cipherText), DefaultEncoding.GetBytes(key)));
        }

        /// <summary>
        /// Encrypts the plain text by using the AES encryption algorithm. 
        /// </summary>
        /// <param name="plainText">The raw data of plain text. </param>
        /// <param name="key">The raw data of key. </param>
        /// <returns>The raw data of cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText or key</exception>
        public static byte[] AesEncrypt(byte[] plainText, byte[] key)
        {
            if (plainText == null || plainText.Length == 0)
                throw new ArgumentNullException(nameof(plainText));

            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));

            using (var rm = new RijndaelManaged { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                using (var transform = rm.CreateEncryptor())
                {
                    return transform.TransformFinalBlock(plainText, 0, plainText.Length);
                }
            }
        }

        /// <summary>
        /// Encrypts the plain text by using the AES encryption algorithm. 
        /// </summary>
        /// <param name="plainText">The plaintext. </param>
        /// <param name="key">The key. </param>
        /// <returns>The cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText or key</exception>
        public static string AesEncrypt(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return DefaultEncoding.GetString(AesEncrypt(DefaultEncoding.GetBytes(plainText), DefaultEncoding.GetBytes(key)));
        }

        /// <summary>
        /// Generates the random key data for encryption. 
        /// </summary>
        /// <param name="length">The length of key string. </param>
        /// <param name="includeNumbers">if set to <c>true</c> [include numbers]. </param>
        /// <param name="includeLowercaseCharacters">if set to <c>true</c> [include lowercase characters]. </param>
        /// <param name="includeUppercaseCharacters">if set to <c>true</c> [include uppercase characters]. </param>
        /// <param name="includeSpecialCharacters">if set to <c>true</c> [include special characters]. </param>
        /// <returns>The key data for encryption. </returns>
        public static byte[] GenerateRandomKey(int length = 16,
            bool includeNumbers = true,
            bool includeLowercaseCharacters = true,
            bool includeUppercaseCharacters = true,
            bool includeSpecialCharacters = true)
        {
            var keyString = GenerateRandomKeyString(length, includeNumbers, includeLowercaseCharacters, includeUppercaseCharacters, includeSpecialCharacters);
            return DefaultEncoding.GetBytes(keyString);
        }

        /// <summary>
        /// Generates the random key string for encryption, which is original plain text that encoding by UTF-8. 
        /// </summary>
        /// <param name="length">The length of key string. </param>
        /// <param name="includeNumbers">if set to <c>true</c> [include numbers]. </param>
        /// <param name="includeLowercaseCharacters">if set to <c>true</c> [include lowercase characters]. </param>
        /// <param name="includeUppercaseCharacters">if set to <c>true</c> [include uppercase characters]. </param>
        /// <param name="includeSpecialCharacters">if set to <c>true</c> [include special characters]. </param>
        /// <returns>The key string for encryption, which is original plain text that encoding by UTF-8. </returns>
        public static string GenerateRandomKeyString(int length = 16,
            bool includeNumbers = true,
            bool includeLowercaseCharacters = true,
            bool includeUppercaseCharacters = true,
            bool includeSpecialCharacters = true)
        {
            var buffer = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(buffer);
            var random = new Random(BitConverter.ToInt32(buffer, 0));
            var result = new StringBuilder();
            var group = new StringBuilder();

            if (includeNumbers)
                group.Append(Numbers);

            if (includeLowercaseCharacters)
                group.Append(LowercaseCharacters);

            if (includeUppercaseCharacters)
                group.Append(UppercaseCharacters);

            if (includeSpecialCharacters)
                group.Append(SpecialCharacters);

            var groupString = group.ToString();

            for (var i = 0; i < length; i++)
            {
                result.Append(groupString.Substring(random.Next(0, groupString.Length - 1), 1));
            }

            return result.ToString();
        }

        /// <summary>
        /// Encrypts the plain text to 16 bit cipher text by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="plainText">The plain text. </param>
        /// <param name="encoding">The <see cref="System.Text.Encoding"/> of plain text. </param>
        /// <param name="lowerCase">if set to <c>true</c> [output cipher text as lowerCase case]. </param>
        /// <returns>The cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText</exception>
        public static string Md5Encrypt(string plainText, Encoding encoding = null, bool lowerCase = true)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (encoding == null)
                encoding = DefaultEncoding;
            
            return Md5Encrypt(encoding.GetBytes(plainText), lowerCase);
        }

        /// <summary>
        /// Encrypts the plain text to cipher text by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="plainText">The plain text. </param>
        /// <param name="encoding">The <see cref="System.Text.Encoding"/> of plain text. </param>
        /// <param name="lowerCase">if set to <c>true</c> [output cipher text as lowerCase case]. </param>
        /// <returns>The cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText</exception>
        public static string Md5HashEncrypt(string plainText, Encoding encoding = null, bool lowerCase = true)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (encoding == null)
                encoding = DefaultEncoding;
            
            return Md5HashEncrypt(encoding.GetBytes(plainText), lowerCase);
        }

        /// <summary>
        /// Encrypts the plain text to 16 bit cipher text in base 64 by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="plainText">The plain text. </param>
        /// <param name="encoding">The <see cref="System.Text.Encoding"/> of plain text. </param>
        /// <returns>The string representation, in base 64, of the contents of the cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText</exception>
        public static string Md5EncryptToBase64(string plainText, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (encoding == null)
                encoding = DefaultEncoding;

            return Md5EncryptToBase64(encoding.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts the plain text to cipher text in base 64 by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="plainText">The plain text. </param>
        /// <param name="encoding">The <see cref="System.Text.Encoding"/> of plain text. </param>
        /// <returns>The string representation, in base 64, of the contents of the cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText</exception>
        public static string Md5HashEncryptToBase64(string plainText, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            
            if (encoding == null)
                encoding = DefaultEncoding;
            
            return Md5HashEncryptToBase64(encoding.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts the raw data of plain text to 16 bit cipher text by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="rawData">The raw data of plain text. </param>
        /// <param name="lowerCase">if set to <c>true</c> [output cipher text as lower case]. </param>
        /// <returns>The cipher text. </returns>
        public static string Md5Encrypt(byte[] rawData, bool lowerCase)
        {
            var cipherTextRawData = Md5HashEncrypt(rawData);
            var result = BitConverter.ToString(cipherTextRawData, 4, 8).Replace("-", string.Empty);
            return lowerCase ? result.ToLower() : result;
        }

        /// <summary>
        /// Encrypts the raw data of plain text to cipher text by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="rawData">The raw data of plain text. </param>
        /// <param name="lowerCase">if set to <c>true</c> [output cipher text as lower case]. </param>
        /// <returns>The cipher text. </returns>
        public static string Md5HashEncrypt(byte[] rawData, bool lowerCase)
        {
            var cipherTextRawData = Md5HashEncrypt(rawData);
            var builder = new StringBuilder();
            const string lowerCaseFormat = "x2";
            const string upperCaseFormat = "X2";
            var format = lowerCase ? lowerCaseFormat : upperCaseFormat;
            foreach (var data in cipherTextRawData)
            {
                builder.Append(data.ToString(format));
            }
            
            return builder.ToString();
        }

        /// <summary>
        /// Encrypts the raw data of plain text to 16 bit cipher text in base 64 by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="rawData">The raw data of plain text. </param>
        /// <returns>The string representation, in base 64, of the 16 bit contents of the cipher text. </returns>
        public static string Md5EncryptToBase64(byte[] rawData)
        {
            var cipherTextRawData = Md5HashEncrypt(rawData);
            var data = new byte[8];
            Array.Copy(cipherTextRawData, 4, data, 0, 8);
            return Convert.ToBase64String(data);
        }
        
        /// <summary>
        /// Encrypts the raw data of plain text to cipher text in base 64 by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="rawData">The raw data of plain text. </param>
        /// <returns>The string representation, in base 64, of the contents of the cipher text. </returns>
        public static string Md5HashEncryptToBase64(byte[] rawData)
        {
            var cipherTextRawData = Md5HashEncrypt(rawData);
            return Convert.ToBase64String(cipherTextRawData);
        }

        /// <summary>
        /// Encrypts the raw data of plain text by using the MD5 Hash encryption algorithm. 
        /// </summary>
        /// <param name="rawData">The raw data of plain text. </param>
        /// <returns>The raw data of cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText</exception>
        public static byte[] Md5HashEncrypt(byte[] rawData)
        {
            if (rawData == null || rawData.Length == 0)
                throw new ArgumentNullException(nameof(rawData));

            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(rawData);
            }
        }

        /// <summary>
        /// Decrypts the cipher text by using the XXTEA encryption algorithm. 
        /// </summary>
        /// <param name="cipherText">The cipher text. </param>
        /// <param name="key">The key. </param>
        /// <returns>The plain text. </returns>
        /// <exception cref="ArgumentNullException">cipherText or key</exception>
        public static string XxteaDecrypt(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return DefaultEncoding.GetString(Xxtea.Decrypt(DefaultEncoding.GetBytes(cipherText), key));
        }

        /// <summary>
        /// Decrypts the raw data of cipher text by using the XXTEA encryption algorithm. 
        /// </summary>
        /// <param name="cipherText">The raw data of cipher text. </param>
        /// <param name="key">The raw data of key. </param>
        /// <returns>The raw data of plain text. </returns>
        /// <exception cref="ArgumentNullException">cipherText or key</exception>
        public static byte[] XxteaDecrypt(byte[] cipherText, byte[] key)
        {
            if (cipherText == null || cipherText.Length == 0)
                throw new ArgumentNullException(nameof(cipherText));

            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));

            return Xxtea.Decrypt(cipherText, key);
        }

        /// <summary>
        /// Encrypts the plain text by using the XXTEA encryption algorithm. 
        /// </summary>
        /// <param name="plainText">The plain text. </param>
        /// <param name="key">The key. </param>
        /// <returns>The cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText or key</exception>
        public static string XxteaEncrypt(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return DefaultEncoding.GetString(Xxtea.Encrypt(plainText, key));
        }

        /// <summary>
        /// Encrypts the raw data of plain text by using the XXTEA encryption algorithm. 
        /// </summary>
        /// <param name="plainText">The raw da of plain text. </param>
        /// <param name="key">The raw data of key. </param>
        /// <returns>The raw data of cipher text. </returns>
        /// <exception cref="ArgumentNullException">plainText or key</exception>
        public static byte[] XxteaEncrypt(byte[] plainText, byte[] key)
        {
            if (plainText == null || plainText.Length == 0)
                throw new ArgumentNullException(nameof(plainText));

            if (key == null || key.Length == 0)
                throw new ArgumentNullException(nameof(key));

            return Xxtea.Encrypt(plainText, key);
        }
    }
}