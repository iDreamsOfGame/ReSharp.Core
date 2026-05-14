// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Provides static methods to generate pseudo-random number.
    /// </summary>
    public static class RandomUtility
    {
        /// <summary>
        /// Gets the <see cref="Random"/> object with random seed initialized.
        /// </summary>
        public static Random RandomWithSeed { get; private set; } = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// Initializes the random number generator with a specified seed.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public static void InitializeWithSeed(int seed)
        {
            RandomWithSeed = new Random(seed);
        }

        /// <summary>
        /// Returns a non-negative random integer on random seed initialized.
        /// </summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="Int32.MaxValue"/>.</returns>
        public static int Next() => RandomWithSeed.Next();

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum on random seed initialized.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. <c>maxValue</c> must be greater than or equal to 0.</param>
        /// <returns>
        ///A 32-bit signed integer that is greater than or equal to 0, and less than <c>maxValue</c>;
        /// that is, the range of return values ordinarily includes 0 but not <c>maxValue</c>. However, if <c>maxValue</c> equals 0, <c>maxValue</c> is returned.
        /// </returns>
        public static int Next(int maxValue) => RandomWithSeed.Next(maxValue);

        /// <summary>
        /// Returns a random integer that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static int Next(int minValue, int maxValue) => RandomWithSeed.Next(minValue, maxValue);

        /// <summary>
        /// Returns a non-negative random integer on random seed initialized.
        /// </summary>
        /// <returns>A 64-bit signed integer that is greater than or equal to 0 and less than <see cref="Int64.MaxValue"/>.</returns>
        public static long NextInt64() => RandomWithSeed.NextInt64();

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers on random seed initialized.
        /// </summary>
        /// <param name="buffer">The array to be filled with random numbers.</param>
        public static void NextBytes(byte[] buffer) => RandomWithSeed.NextBytes(buffer);

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum on random seed initialized.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. <c>maxValue</c> must be greater than or equal to 0.</param>
        /// <returns>
        ///A 64-bit signed integer that is greater than or equal to 0, and less than <c>maxValue</c>;
        /// that is, the range of return values ordinarily includes 0 but not <c>maxValue</c>. However, if <c>maxValue</c> equals 0, <c>maxValue</c> is returned.
        /// </returns>
        public static long NextInt64(long maxValue) => RandomWithSeed.NextInt64(maxValue);

        /// <summary>
        /// Returns a random integer that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static long NextInt64(long minValue, long maxValue) => RandomWithSeed.NextInt64(minValue, maxValue);

        /// <summary>
        /// Returns a non-negative random unsigned 64-bit integer on random seed initialized.
        /// </summary>
        /// <returns>A 64-bit unsigned integer that is greater than or equal to 0 and less than <see cref="UInt64.MaxValue"/>.</returns>
        public static ulong NextUInt64() => RandomWithSeed.NextUInt64();

        /// <summary>
        /// Returns a non-negative random unsigned 64-bit integer that is less than the specified maximum on random seed initialized.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. <c>maxValue</c> must be greater than or equal to 0.</param>
        /// <returns>
        /// A 64-bit unsigned integer that is greater than or equal to 0, and less than <c>maxValue</c>;
        /// that is, the range of return values ordinarily includes 0 but not <c>maxValue</c>. However, if <c>maxValue</c> equals 0, <c>maxValue</c> is returned.
        /// </returns>
        public static ulong NextUInt64(ulong maxValue) => RandomWithSeed.NextUInt64(maxValue);

        /// <summary>
        /// Returns a random unsigned 64-bit integer that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A 64-bit unsigned integer greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static ulong NextUInt64(ulong minValue, ulong maxValue) => RandomWithSeed.NextUInt64(minValue, maxValue);

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0 on random seed initialized.
        /// </summary>
        /// <returns>A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public static float NextSingle() => RandomWithSeed.NextSingle();

        /// <summary>
        /// Returns a random floating-point number that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A single-precision floating point number greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static float NextSingle(float minValue, float maxValue) => RandomWithSeed.NextSingle(minValue, maxValue);

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0 on random seed initialized.
        /// </summary>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public static double NextDouble() => RandomWithSeed.NextDouble(0, 1);

        /// <summary>
        /// Returns a random floating-point number that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A double-precision floating point number greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static double NextDouble(double minValue, double maxValue) => RandomWithSeed.NextDouble(minValue, maxValue);

        /// <summary>
        /// Returns a sequence of random integers within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <param name="count">The number of random integers to generate.</param>
        /// <returns>An array of 32-bit signed integers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static int[] NextSequence(int minValue, int maxValue, int count) => RandomWithSeed.NextSequence(minValue, maxValue, count);

        /// <summary>
        /// Returns a sequence of random 64-bit integers within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <param name="count">The number of random 64-bit integers to generate.</param>
        /// <returns>An array of 64-bit signed integers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static long[] NextInt64Sequence(long minValue, long maxValue, int count) => RandomWithSeed.NextInt64Sequence(minValue, maxValue, count);

        /// <summary>
        /// Returns a sequence of random unsigned 64-bit integers within a specific range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <param name="count">The number of random unsigned 64-bit integers to generate.</param>
        /// <returns>An array of random unsigned 64-bit integers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static ulong[] NextUInt64Sequence(ulong minValue, ulong maxValue, int count) => RandomWithSeed.NextUInt64Sequence(minValue, maxValue, count);

        /// <summary>
        /// Returns a sequence of random single-precision floating-point numbers within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <param name="count">The number of random single-precision floating-point numbers to generate.</param>
        /// <returns>An array of single-precision floating-point numbers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static float[] NextSingleSequence(float minValue, float maxValue, int count) => RandomWithSeed.NextSingleSequence(minValue, maxValue, count);

        /// <summary>
        /// Returns a sequence with random floating-point numbers that are greater than or equal to <b>minValue</b>, and less than <b>maxValue</b>.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random floating-point numbers returned. </param>
        /// <param name="maxValue">The exclusive upper bound of the random floating-point numbers returned.
        /// <b>maxValue</b> must be greater than or equal to <b>minValue</b>. </param>
        /// <param name="count">The count of sequence. </param>
        /// <returns>A sequence with random floating-point number are greater than or equal to <b>minValue</b>, and less than <b>maxValue</b>. </returns>
        /// <exception cref="ArgumentOutOfRangeException"><b>minValue</b> is greater than <b>maxValue</b>. </exception>
        public static double[] NextDoubleSequence(double minValue, double maxValue, int count) => RandomWithSeed.NextDoubleSequence(minValue, maxValue, count);
    }
}