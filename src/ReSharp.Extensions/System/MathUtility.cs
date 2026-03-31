// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Security.Cryptography;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Provides constants and static methods for trigonometric, logarithmic, and other common
    /// mathematical functions. This class cannot be inherited.
    /// </summary>
    public static class MathUtility
    {
        /// <summary>
        /// Generates the random seed.
        /// </summary>
        /// <returns>The random seed.</returns>
        public static int GenerateRandomSeed()
        {
            var bytes = new byte[4];
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Generate Gaussian Random Number.
        /// </summary>
        /// <returns>The Gaussian Random Number.</returns>
        public static float GenGaussianRandomNumber()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var x1 = (float)random.NextDouble();
            var x2 = (float)random.NextDouble();
            if (x1 == 0.0f)
                x1 = 0.01f;

            return (float)(Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2));
        }

        /// <summary>
        /// Gets the reciprocal of a number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The reciprocal of the number.</returns>
        public static float GetReciprocal(float number) => 1.0f / number;

        /// <summary>
        /// Determines whether the specific <see cref="int"/> is odd.
        /// </summary>
        /// <param name="n">The <see cref="int"/>.</param>
        /// <returns><c>true</c> if the specific <see cref="int"/> is odd; otherwise, <c>false</c>.</returns>
        public static bool IsOdd(int n) => Convert.ToBoolean(n & 1);

        /// <summary>
        /// Determines whether the specific <see cref="float"/> is odd.
        /// </summary>
        /// <param name="n">The <see cref="float"/>.</param>
        /// <returns><c>true</c> if the specific <see cref="float"/> is odd; otherwise, <c>false</c>.</returns>
        public static bool IsOdd(float n) => IsOdd((int)Math.Floor(n));
    }
}