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
        public static long RangeInt64(this Random source) => source.RangeInt64(0, long.MaxValue);
        
        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random 64-bit signed integer.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        /// <returns>
        /// A 64-bit signed integer that is greater than or equal to 0, and less than <c>maxValue</c>;
        /// that is, the range of return values ordinarily includes 0 but not <c>maxValue</c>. However, if <c>maxValue</c> equals 0, <c>maxValue</c> is returned.
        /// </returns>
        public static long RangeInt64(this Random source, long maxValue) => source.RangeInt64(0, maxValue);

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
        public static long RangeInt64(this Random source, long minValue, long maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), "minValue is greater than maxValue.");

            var range = maxValue - minValue;
            var result = source.NextDouble() * range + minValue;

            if (double.IsPositiveInfinity(result))
            {
                result = long.MaxValue;
            }
            else if (double.IsNegativeInfinity(result))
            {
                result = long.MinValue;
            }

            return (long)result;
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <param name="source">The <see cref="Random"/> to return a random floating-point number.</param>
        /// <returns>A single-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public static float RangeSingle(this Random source) => source.RangeSingle(0, 1);

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
        public static float RangeSingle(this Random source, float minValue, float maxValue)
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
        public static double RangeDouble(this Random source, double minValue, double maxValue)
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
    }
}