// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="Random"/>.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random 64-bit signed integer.</param>
        /// <returns>A 64-bit signed integer that is greater than or equal to 0 and less than <see cref="Int64.MaxValue"/>.</returns>
        public static long NextInt64(this Random source)
        {
            var buf = new byte[8];
            source.NextBytes(buf);
            return BitConverter.ToInt64(buf, 0);
        }

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random 64-bit signed integer.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        /// <returns>
        /// A 64-bit signed integer that is greater than or equal to 0, and less than <c>maxValue</c>;
        /// that is, the range of return values ordinarily includes 0 but not <c>maxValue</c>. However, if <c>maxValue</c> equals 0, <c>maxValue</c> is returned.
        /// </returns>
        public static long NextInt64(this Random source, long maxValue) => source.NextInt64(0, maxValue);

        /// <summary>
        /// Returns a random 64-bit signed integer that is within a specific range.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random 64-bit signed integer.</param>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to <c>minValue</c> and less than
        /// <c>maxValue</c>; that is, the range of return values includes <c>minValue</c> but not
        /// <c>maxValue</c>. If <c>minValue</c> equals <c>maxValue</c>, <c>minValue</c> is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static long NextInt64(this Random source, long minValue, long maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue is greater than maxValue.");

            if (minValue == maxValue)
                return minValue;

            var diff = (ulong)(maxValue - minValue);
            var rand = source.NextUInt64();
            return minValue + (long)(rand % diff);
        }

        /// <summary>
        /// Returns a random unsigned 64-bit integer.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random unsigned 64-bit integer.</param>
        /// <returns>An unsigned 64-bit integer that is greater than or equal to 0 and less than <see cref="UInt64.MaxValue"/>.</returns>
        public static ulong NextUInt64(this Random source)
        {
            var buf = new byte[8];
            source.NextBytes(buf);
            return BitConverter.ToUInt64(buf, 0);
        }

        /// <summary>
        /// Returns a random unsigned 64-bit integer that is less than the specified maximum.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random unsigned 64-bit integer.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        /// <returns>
        /// An unsigned 64-bit integer that is greater than or equal to 0, and less than <c>maxValue</c>;
        /// that is, the range of return values ordinarily includes 0 but not <c>maxValue</c>. However, if <c>maxValue</c> equals 0, <c>maxValue</c> is returned.
        /// </returns>
        public static ulong NextUInt64(this Random source, ulong maxValue) => source.NextUInt64(0, maxValue);

        /// <summary>
        /// Returns a random unsigned 64-bit integer that is within a specific range.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random unsigned 64-bit integer.</param>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <returns>
        /// An unsigned 64-bit integer greater than or equal to <c>minValue</c> and less than
        /// <c>maxValue</c>; that is, the range of return values includes <c>minValue</c> but not
        /// <c>maxValue</c>. If <c>minValue</c> equals <c>maxValue</c>, <c>minValue</c> is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static ulong NextUInt64(this Random source, ulong minValue, ulong maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue is greater than maxValue.");

            if (minValue == maxValue)
                return minValue;
            
            var diff = maxValue - minValue;
            var rand = source.NextUInt64();
            return minValue + rand % diff;
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random floating-point number.</param>
        /// <returns>A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public static float NextSingle(this Random source) => source.NextSingle(0, 1);

        /// <summary>
        /// Returns a random single that is within a specific range.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a single double.</param>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <returns>
        /// A single greater than or equal to <c>minValue</c> and less than <c>maxValue</c>;
        /// that is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>.
        /// If <c>minValue</c> equals <c>maxValue</c>, <c>minValue</c> is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static float NextSingle(this Random source, float minValue, float maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue is greater than maxValue.");

            var range = maxValue - minValue;
            float result;

            if (range <= float.MaxValue)
            {
                result = (float)(source.NextDouble() * range + minValue);
            }
            else
            {
                result = (float)(source.NextDouble() * float.MaxValue + minValue);
            }

            if (float.IsPositiveInfinity(result))
            {
                result = float.MaxValue;
            }
            else if (float.IsNegativeInfinity(result))
            {
                result = float.MinValue;
            }

            return result;
        }

        /// <summary>
        /// Returns a random double that is within a specific range.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random <see cref="double"/> value.</param>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <returns>
        /// A double greater than or equal to <c>minValue</c> and less than <c>maxValue</c>; that
        /// is, the range of return values includes <c>minValue</c> but not <c>maxValue</c>. If
        /// <c>minValue</c> equals <c>maxValue</c>, <c>minValue</c> is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static double NextDouble(this Random source, double minValue, double maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue is greater than maxValue.");

            var range = maxValue - minValue;
            double result;

            if (range <= double.MaxValue)
            {
                result = source.NextDouble() * range + minValue;
            }
            else
            {
                result = source.NextDouble() * double.MaxValue + minValue;
            }

            if (double.IsPositiveInfinity(result))
            {
                result = double.MaxValue;
            }
            else if (double.IsNegativeInfinity(result))
            {
                result = double.MinValue;
            }

            return result;
        }

        /// <summary>
        /// Returns a sequence of random integers within a specific range.
        /// </summary>
        /// <param name="random">The <see cref="Random"/> to return random integers.</param>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <param name="count">The number of random integers to generate.</param>
        /// <returns>An array of random integers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static int[] NextSequence(this Random random,
            int minValue,
            int maxValue,
            int count)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));

            var sequence = new int[count];
            var range = maxValue - minValue;
            var unitValue = range / count;

            var i = 0;
            var startValue = minValue;
            while (i < count)
            {
                var endValue = Math.Min(startValue + unitValue, maxValue);
                sequence[i] = random.Next(startValue, endValue);
                startValue = endValue;
                i++;
            }

            return sequence;
        }

        /// <summary>
        /// Returns a sequence of random 64-bit signed integers within a specific range.
        /// </summary>
        /// <param name="random">The <see cref="Random"/> to return random 64-bit signed integers.</param>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <param name="count">The number of random 64-bit signed integers to generate.</param>
        /// <returns>An array of random 64-bit signed integers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static long[] NextInt64Sequence(this Random random,
            long minValue,
            long maxValue,
            int count)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));

            var sequence = new long[count];
            var range = maxValue - minValue;
            var unitValue = range / count;

            var i = 0;
            var startValue = minValue;
            while (i < count)
            {
                var endValue = Math.Min(startValue + unitValue, maxValue);
                sequence[i] = random.NextInt64(startValue, endValue);
                startValue = endValue;
                i++;
            }

            return sequence;
        }
        
        /// <summary>
        /// Returns a sequence of random unsigned 64-bit integers within a specific range.
        /// </summary>
        /// <param name="random">The <see cref="Random"/> to return random unsigned 64-bit integers.</param>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <param name="count">The number of random unsigned 64-bit integers to generate.</param>
        /// <returns>An array of random unsigned 64-bit integers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static ulong[] NextUInt64Sequence(this Random random, 
            ulong minValue,
            ulong maxValue,
            int count)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));

            var sequence = new ulong[count];
            var range = maxValue - minValue;
            var unitValue = range / (ulong)count;

            var i = 0;
            var startValue = minValue;
            while (i < count)
            {
                var endValue = Math.Min(startValue + unitValue, maxValue);
                sequence[i] = random.NextUInt64(startValue, endValue);
                startValue = endValue;
                i++;
            }

            return sequence;
        }

        /// <summary>
        /// Returns a sequence of random single-precision floating-point numbers within a specific range.
        /// </summary>
        /// <param name="random">The <see cref="Random"/> to return random single-precision floating-point numbers.</param>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <param name="count">The number of random single-precision floating-point numbers to generate.</param>
        /// <returns>An array of random single-precision floating-point numbers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static float[] NextSingleSequence(this Random random,
            float minValue,
            float maxValue,
            int count)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));

            var sequence = new float[count];
            var range = maxValue - minValue;
            var unitValue = range / count;

            var i = 0;
            var startValue = minValue;
            while (i < count)
            {
                var endValue = Math.Min(startValue + unitValue, maxValue);
                sequence[i] = random.NextSingle(startValue, endValue);
                startValue = endValue;
                i++;
            }

            return sequence;
        }

        /// <summary>
        /// Returns a sequence of random double-precision floating-point numbers within a specific range.
        /// </summary>
        /// <param name="random">The <see cref="Random"/> to return random double-precision floating-point numbers.</param>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random numbers returned. <c>maxValue</c> must be greater
        /// than or equal to <c>minValue</c>.
        /// </param>
        /// <param name="count">The number of random double-precision floating-point numbers to generate.</param>
        /// <returns>An array of random double-precision floating-point numbers greater than or equal to <c>minValue</c> and less than <c>maxValue</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>minValue</c> is greater than <c>maxValue</c>.</exception>
        public static double[] NextDoubleSequence(this Random random,
            double minValue,
            double maxValue,
            int count)
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
                sequence[i] = random.NextDouble(startValue, endValue);
                startValue = endValue;
                i++;
            }

            return sequence;
        }
    }
}