// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Text;

namespace ReSharp.Security.Cryptography
{
    internal static class Xxtea
    {
        #region Fields

        private const uint Delta = 0x9E3779B9;

        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        #endregion Fields

        #region Methods

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            if (data.Length == 0)
            {
                return data;
            }
            return ToByteArray(Decrypt(ToUInt32Array(data, false), ToUInt32Array(FixKey(key), false)), true);
        }

        public static byte[] Decrypt(byte[] data, string key)
        {
            return Decrypt(data, DefaultEncoding.GetBytes(key));
        }

        public static byte[] DecryptBase64String(string data, byte[] key)
        {
            return Decrypt(Convert.FromBase64String(data), key);
        }

        public static byte[] DecryptBase64String(string data, string key)
        {
            return Decrypt(Convert.FromBase64String(data), key);
        }

        public static string DecryptToString(byte[] data, byte[] key)
        {
            return DefaultEncoding.GetString(Decrypt(data, key));
        }

        public static string DecryptToString(byte[] data, string key)
        {
            return DefaultEncoding.GetString(Decrypt(data, key));
        }

        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data.Length == 0)
            {
                return data;
            }
            return ToByteArray(Encrypt(ToUInt32Array(data, true), ToUInt32Array(FixKey(key), false)), false);
        }

        public static byte[] Encrypt(string data, byte[] key)
        {
            return Encrypt(DefaultEncoding.GetBytes(data), key);
        }

        public static byte[] Encrypt(byte[] data, string key)
        {
            return Encrypt(data, DefaultEncoding.GetBytes(key));
        }

        public static byte[] Encrypt(string data, string key)
        {
            return Encrypt(DefaultEncoding.GetBytes(data), DefaultEncoding.GetBytes(key));
        }

        public static string EncryptToBase64String(byte[] data, byte[] key)
        {
            return Convert.ToBase64String(Encrypt(data, key));
        }

        public static string EncryptToBase64String(string data, byte[] key)
        {
            return Convert.ToBase64String(Encrypt(data, key));
        }

        public static string EncryptToBase64String(byte[] data, string key)
        {
            return Convert.ToBase64String(Encrypt(data, key));
        }

        public static string EncryptToBase64String(string data, string key)
        {
            return Convert.ToBase64String(Encrypt(data, key));
        }

        private static uint[] Decrypt(uint[] v, uint[] k)
        {
            int n = v.Length - 1;
            if (n < 1)
            {
                return v;
            }
            uint z, y = v[0], sum, e;
            int p, q = 6 + 52 / (n + 1);
            unchecked
            {
                sum = (uint)(q * Delta);
                while (sum != 0)
                {
                    e = sum >> 2 & 3;
                    for (p = n; p > 0; p--)
                    {
                        z = v[p - 1];
                        y = v[p] -= MX(sum, y, z, p, e, k);
                    }
                    z = v[n];
                    y = v[0] -= MX(sum, y, z, p, e, k);
                    sum -= Delta;
                }
            }
            return v;
        }

        private static uint[] Encrypt(uint[] v, uint[] k)
        {
            int n = v.Length - 1;
            if (n < 1)
            {
                return v;
            }
            uint z = v[n], y, sum = 0, e;
            int p, q = 6 + 52 / (n + 1);
            unchecked
            {
                while (0 < q--)
                {
                    sum += Delta;
                    e = sum >> 2 & 3;
                    for (p = 0; p < n; p++)
                    {
                        y = v[p + 1];
                        z = v[p] += MX(sum, y, z, p, e, k);
                    }
                    y = v[0];
                    z = v[n] += MX(sum, y, z, p, e, k);
                }
            }
            return v;
        }

        private static byte[] FixKey(byte[] key)
        {
            if (key.Length == 16)
                return key;
            byte[] fixedkey = new byte[16];
            if (key.Length < 16)
            {
                key.CopyTo(fixedkey, 0);
            }
            else
            {
                Array.Copy(key, 0, fixedkey, 0, 16);
            }
            return fixedkey;
        }

        private static uint MX(uint sum, uint y, uint z, int p, uint e, uint[] k)
        {
            return (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z);
        }

        private static byte[] ToByteArray(uint[] data, bool includeLength)
        {
            int n = data.Length << 2;
            if (includeLength)
            {
                int m = (int)data[data.Length - 1];
                n -= 4;
                if ((m < n - 3) || (m > n))
                {
                    return null;
                }
                n = m;
            }
            byte[] result = new byte[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = (byte)(data[i >> 2] >> ((i & 3) << 3));
            }
            return result;
        }

        private static uint[] ToUInt32Array(byte[] data, bool includeLength)
        {
            int length = data.Length;
            int n = (((length & 3) == 0) ? (length >> 2) : ((length >> 2) + 1));
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
            for (int i = 0; i < length; i++)
            {
                result[i >> 2] |= (uint)data[i] << ((i & 3) << 3);
            }
            return result;
        }

        #endregion Methods
    }
}