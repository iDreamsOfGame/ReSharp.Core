// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Text;

// ReSharper disable UseIndexFromEndExpression

namespace ReSharp.Security.Cryptography
{
    internal static class Xxtea
    {
        private const uint Delta = 0x9E3779B9;

        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        public static byte[] Decrypt(byte[] data, byte[] key) => data.Length == 0 ? data : ToByteArray(Decrypt(ToUInt32Array(data, false), ToUInt32Array(FixKey(key), false)), true);

        public static byte[] Decrypt(byte[] data, string key) => Decrypt(data, DefaultEncoding.GetBytes(key));

        public static byte[] DecryptBase64String(string data, byte[] key) => Decrypt(Convert.FromBase64String(data), key);

        public static byte[] DecryptBase64String(string data, string key) => Decrypt(Convert.FromBase64String(data), key);

        public static string DecryptToString(byte[] data, byte[] key) => DefaultEncoding.GetString(Decrypt(data, key));

        public static string DecryptToString(byte[] data, string key) => DefaultEncoding.GetString(Decrypt(data, key));

        public static byte[] Encrypt(byte[] data, byte[] key) => 
            data.Length == 0 
                ? data
                : ToByteArray(Encrypt(ToUInt32Array(data, true), ToUInt32Array(FixKey(key), false)), false);

        public static byte[] Encrypt(string data, byte[] key) => Encrypt(DefaultEncoding.GetBytes(data), key);

        public static byte[] Encrypt(byte[] data, string key) => Encrypt(data, DefaultEncoding.GetBytes(key));

        public static byte[] Encrypt(string data, string key) => Encrypt(DefaultEncoding.GetBytes(data), DefaultEncoding.GetBytes(key));

        public static string EncryptToBase64String(byte[] data, byte[] key) => Convert.ToBase64String(Encrypt(data, key));

        public static string EncryptToBase64String(string data, byte[] key) => Convert.ToBase64String(Encrypt(data, key));

        public static string EncryptToBase64String(byte[] data, string key) => Convert.ToBase64String(Encrypt(data, key));

        public static string EncryptToBase64String(string data, string key) => Convert.ToBase64String(Encrypt(data, key));

        private static uint[] Decrypt(uint[] v, uint[] k)
        {
            var n = v.Length - 1;
            if (n < 1)
            {
                return v;
            }

            var y = v[0];
            var q = 6 + 52 / (n + 1);
            unchecked
            {
                var sum = (uint)(q * Delta);
                while (sum != 0)
                {
                    var e = sum >> 2 & 3;
                    int p;
                    uint z;
                    for (p = n; p > 0; p--)
                    {
                        z = v[p - 1];
                        y = v[p] -= Mx(sum, y, z, p, e, k);
                    }

                    z = v[n];
                    y = v[0] -= Mx(sum, y, z, p, e, k);
                    sum -= Delta;
                }
            }

            return v;
        }

        private static uint[] Encrypt(uint[] v, uint[] k)
        {
            var n = v.Length - 1;
            if (n < 1)
            {
                return v;
            }

            var z = v[n];
            uint sum = 0;
            var q = 6 + 52 / (n + 1);
            unchecked
            {
                while (0 < q--)
                {
                    sum += Delta;
                    var e = sum >> 2 & 3;
                    int p;
                    uint y;
                    for (p = 0; p < n; p++)
                    {
                        y = v[p + 1];
                        z = v[p] += Mx(sum, y, z, p, e, k);
                    }

                    y = v[0];
                    z = v[n] += Mx(sum, y, z, p, e, k);
                }
            }

            return v;
        }

        private static byte[] FixKey(byte[] key)
        {
            if (key.Length == 16)
                return key;
            var fixedKey = new byte[16];
            if (key.Length < 16)
            {
                key.CopyTo(fixedKey, 0);
            }
            else
            {
                Array.Copy(key, 0, fixedKey, 0, 16);
            }

            return fixedKey;
        }

        private static uint Mx(uint sum,
            uint y,
            uint z,
            int p,
            uint e,
            uint[] k) =>
            (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z);

        private static byte[] ToByteArray(uint[] data, bool includeLength)
        {
            var n = data.Length << 2;
            if (includeLength)
            {
                var m = (int)data[data.Length - 1];
                n -= 4;
                if (m < n - 3 || m > n)
                {
                    return null;
                }

                n = m;
            }

            var result = new byte[n];
            for (var i = 0; i < n; i++)
            {
                result[i] = (byte)(data[i >> 2] >> ((i & 3) << 3));
            }

            return result;
        }

        private static uint[] ToUInt32Array(byte[] data, bool includeLength)
        {
            var length = data.Length;
            var n = (length & 3) == 0 ? length >> 2 : (length >> 2) + 1;
            uint[] result;
            if (includeLength)
            {
                result = new uint[n + 1];
                result[n] = (uint)length;
            }
            else
            {
                result = new uint[n];
            }

            for (var i = 0; i < length; i++)
            {
                result[i >> 2] |= (uint)data[i] << ((i & 3) << 3);
            }

            return result;
        }
    }
}