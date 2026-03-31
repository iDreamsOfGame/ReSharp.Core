// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Security.DataProtection
{
    internal static class DataProtectionProvider
    {
        private static readonly int CheckKey;

        private static readonly long CheckLongKey;

        private static readonly int Key;

        private static readonly long LongKey;

        static DataProtectionProvider()
        {
            var seed = Guid.NewGuid().ToString().GetHashCode();
            var random = new Random(seed);
            Key = random.Next(int.MinValue, int.MaxValue);
            LongKey = ((long)Key << 32) + Key;
            CheckKey = random.Next(int.MinValue, int.MaxValue);
            CheckLongKey = ((long)CheckKey << 32) + CheckKey;
        }
        
        internal static long Protect(double value, out long check)
        {
            var result = BitConverter.DoubleToInt64Bits(value);
            return Protect(result, out check);
        }
        
        internal static int Protect(float value, out int check)
        {
            var result = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
            return Protect(result, out check);
        }
        
        internal static long Protect(long value, out long check)
        {
            var result = value ^ LongKey;
            check = value ^ CheckLongKey;
            return result;
        }
        
        internal static int Protect(int value, out int check)
        {
            var result = value ^ Key;
            check = value ^ CheckKey;
            return result;
        }
        
        internal static int Unprotect(int value, int check)
        {
            var result = value ^ Key;
            check ^= CheckKey;

            if (result == check)
            {
                return result;
            }

            throw new InvalidSecretDataException(result, check, Key, CheckKey);
        }
        
        internal static long Unprotect(long value, long check)
        {
            var result = value ^ LongKey;
            check ^= CheckLongKey;

            if (result == check)
            {
                return result;
            }

            throw new InvalidSecretDataException(result, check, LongKey, CheckLongKey);
        }
        
        internal static double UnprotectDouble(long value, long check)
        {
            var result = Unprotect(value, check);
            return BitConverter.Int64BitsToDouble(result);
        }
        
        internal static float UnprotectSingle(int value, int check)
        {
            var result = Unprotect(value, check);
            return BitConverter.ToSingle(BitConverter.GetBytes(result), 0);
        }
    }
}