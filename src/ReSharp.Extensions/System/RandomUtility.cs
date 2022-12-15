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
        public static Random RandomWithSeed => new Random(Guid.NewGuid().GetHashCode());
        
        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers on random seed initialized.
        /// </summary>
        /// <param name="buffer">The array to be filled with random numbers.</param>
        public static void NextBytesWithRandomSeed(byte[] buffer) => RandomWithSeed.NextBytes(buffer);
        
        /// <summary>
        /// Returns a non-negative random integer on random seed initialized.
        /// </summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="Int32.MaxValue"/>.</returns>
        public static int NextWithRandomSeed() => RandomWithSeed.Next();
        
        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum on random seed initialized.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. <c>maxValue</c> must be greater than or equal to 0.</param>
        /// <returns>
        ///A 32-bit signed integer that is greater than or equal to 0, and less than <c>maxValue</c>;
        /// that is, the range of return values ordinarily includes 0 but not <c>maxValue</c>. However, if <c>maxValue</c> equals 0, <c>maxValue</c> is returned.
        /// </returns>
        public static int NextWithRandomSeed(int maxValue) => RandomWithSeed.Next(maxValue);
        
        /// <summary>
        /// Returns a random integer that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static int NextWithRandomSeed(int minValue, int maxValue) => RandomWithSeed.Next(minValue, maxValue);

        /// <summary>
        /// Returns a non-negative random integer on random seed initialized.
        /// </summary>
        /// <returns>A 64-bit signed integer that is greater than or equal to 0 and less than <see cref="Int64.MaxValue"/>.</returns>
        public static long NextInt64WithRandomSeed() => RandomWithSeed.RangeInt64();
        
        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum on random seed initialized.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. <c>maxValue</c> must be greater than or equal to 0.</param>
        /// <returns>
        ///A 64-bit signed integer that is greater than or equal to 0, and less than <c>maxValue</c>;
        /// that is, the range of return values ordinarily includes 0 but not <c>maxValue</c>. However, if <c>maxValue</c> equals 0, <c>maxValue</c> is returned.
        /// </returns>
        public static long NextInt64WithRandomSeed(long maxValue) => RandomWithSeed.RangeInt64(maxValue);
        
        /// <summary>
        /// Returns a random integer that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static long NextInt64WithRandomSeed(long minValue, long maxValue) => RandomWithSeed.RangeInt64(minValue, maxValue);

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0 on random seed initialized.
        /// </summary>
        /// <returns>A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public static float NextSingleWithRandomSeed() => RandomWithSeed.RangeSingle();
        
        /// <summary>
        /// Returns a random floating-point number that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A single-precision floating point number greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static float NextSingleWithRandomSeed(float minValue, float maxValue) => RandomWithSeed.RangeSingle(minValue, maxValue);
        
        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0 on random seed initialized.
        /// </summary>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public static double NextDoubleWithRandomSeed() => RandomWithSeed.RangeDouble(0, 1);
        
        /// <summary>
        /// Returns a random floating-point number that is within a specified range on random seed initialized.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater than or equal to <c>minValue</c>.</param>
        /// <returns>
        /// A double-precision floating point number greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If minValue equals <c>maxValue</c>, minValue is returned.
        /// </returns>
        public static double NextDoubleWithRandomSeed(double minValue, double maxValue) => RandomWithSeed.RangeDouble(minValue, maxValue);

        /// <summary>
        /// Returns a sequence with random floating-point numbers that are greater than or equal to <b>minValue</b>, and less than <b>maxValue</b>.
        /// </summary>
        /// <param name="count">The count of sequence. </param>
        /// <param name="minValue">The inclusive lower bound of the random floating-point numbers returned. </param>
        /// <param name="maxValue">The exclusive upper bound of the random floating-point numbers returned.
        /// <b>maxValue</b> must be greater than or equal to <b>minValue</b>. </param>
        /// <returns>A sequence with random floating-point number are greater than or equal to <b>minValue</b>, and less than <b>maxValue</b>. </returns>
        /// <exception cref="ArgumentOutOfRangeException"><b>minValue</b> is greater than <b>maxValue</b>. </exception>
        public static double[] NextDoubleSequence(int count, double minValue, double maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));
            
            var sequence = new double[count];
            var range = maxValue - minValue;
            var unitValue = range / count;

            var i = 0;
            var startValue = minValue;
            while (i < count)
            {
                var endValue = Math.Min(startValue + unitValue, maxValue);
                sequence[i] = NextDoubleWithRandomSeed(startValue, endValue);
                startValue = endValue;
                i++;
            }

            return sequence;
        }
    }
}