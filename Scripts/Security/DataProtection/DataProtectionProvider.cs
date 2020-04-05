// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;

namespace ReSharp.Security.DataProtection
{
    internal static class DataProtectionProvider
    {
        #region Fields

        private static readonly int CheckKey;

        private static readonly long CheckLongKey;

        private static readonly int Key;

        private static readonly long LongKey;

        #endregion Fields

        #region Constructors

        static DataProtectionProvider()
        {
            int seed = MathUtility.GenerateRandomSeed();
            Random rnd = new Random(seed);
            int minValue = int.MinValue;
            int maxValue = int.MaxValue;
            Key = rnd.Next(int.MinValue, int.MaxValue);
            LongKey = ((long)Key << 32) + Key;
            CheckKey = rnd.Next(minValue, maxValue);
            CheckLongKey = ((long)CheckKey << 32) + CheckKey;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Encrypts the <see cref="double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to encrypt.</param>
        /// <param name="check">The check for the <see cref="double"/> value.</param>
        /// <returns>The encrypted <see cref="double"/> value.</returns>
        internal static long Protect(double value, out long check)
        {
            long result = BitConverter.DoubleToInt64Bits(value);
            return Protect(result, out check);
        }

        /// <summary>
        /// Encrypts the <see cref="float"/> value.
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to encrypt.</param>
        /// <param name="check">The check for the <see cref="float"/> value.</param>
        /// <returns>The encrypted <see cref="float "/> value.</returns>
        internal static int Protect(float value, out int check)
        {
            int result = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
            return Protect(result, out check);
        }

        /// <summary>
        /// Encrypts the <see cref="long"/> value.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value to encrypt.</param>
        /// <param name="check">The check for the <see cref="long"/> value.</param>
        /// <returns>The encrypted <see cref="long"/> value.</returns>
        internal static long Protect(long value, out long check)
        {
            long result = (value ^ LongKey);
            check = (value ^ CheckLongKey);
            return result;
        }

        /// <summary>
        /// Encrypts the <see cref="int"/> value.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to encrypt.</param>
        /// <param name="check">The check for the <see cref="int"/> value.</param>
        /// <returns>The encrypted <see cref="int"/> value.</returns>
        internal static int Protect(int value, out int check)
        {
            int result = (value ^ Key);
            check = (value ^ CheckKey);
            return result;
        }

        /// <summary>
        /// Decrypts the <see cref="int"/> value with encryption.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to decrpt.</param>
        /// <param name="check">The check for the original <see cref="int"/> value.</param>
        /// <returns>The decrypted <see cref="int"/> value.</returns>
        /// <exception cref="InvalidSecretDataException">
        /// If the <see cref="int"/> value with encryption has been modified.
        /// </exception>
        internal static int Unprotect(int value, int check)
        {
            int result = value ^ Key;
            check ^= CheckKey;

            if (result == check)
            {
                return result;
            }

            throw new InvalidSecretDataException(result, check, Key, CheckKey);
        }

        /// <summary>
        /// Decrypts the <see cref="long"/> value with encryption.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value with encryption.</param>
        /// <param name="check">The check for the original <see cref="long"/> value.</param>
        /// <returns>The decrypted <see cref="long"/> value.</returns>
        /// <exception cref="InvalidSecretDataException">
        /// If the <see cref="long"/> data with encryption has been modified.
        /// </exception>
        internal static long Unprotect(long value, long check)
        {
            long result = value ^ LongKey;
            check ^= CheckLongKey;

            if (result == check)
            {
                return result;
            }

            throw new InvalidSecretDataException(result, check, LongKey, CheckLongKey);
        }

        /// <summary>
        /// Decrypts the <see cref="double"/> value with encryption.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value with encryption.</param>
        /// <param name="check">The check for the original <see cref="double"/> value.</param>
        /// <returns>The decrypted <see cref="double"/> value.</returns>
        internal static double UnprotectDouble(long value, long check)
        {
            long result = Unprotect(value, check);
            return BitConverter.Int64BitsToDouble(result);
        }

        /// <summary>
        /// Decrypts the <see cref="float"/> value with encryption.
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to decrypt.</param>
        /// <param name="check">The check for the original <see cref="float"/> value.</param>
        /// <returns>The encrypted <see cref="float"/> value.</returns>
        internal static float UnprotectSingle(int value, int check)
        {
            int result = Unprotect(value, check);
            return BitConverter.ToSingle(BitConverter.GetBytes(result), 0);
        }

        #endregion Methods
    }
}