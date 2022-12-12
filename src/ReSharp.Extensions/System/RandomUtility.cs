// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace System
{
    /// <summary>
    /// Provides static methods to generate pseudo-random number.
    /// </summary>
    public static class RandomUtility
    {
        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to <b>minValue</b>, and less than <b>maxValue</b>.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random floating-point number returned. </param>
        /// <param name="maxValue">The exclusive upper bound of the random floating-point number returned.
        /// <b>maxValue</b> must be greater than or equal to <b>minValue</b>. </param>
        /// <returns>A floating-point number is greater than or equal to <b>minValue</b>, and less than <b>maxValue</b>. </returns>
        /// <exception cref="ArgumentOutOfRangeException"><b>minValue</b> is greater than <b>maxValue</b>. </exception>
        public static double NextDouble(double minValue, double maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));
            
            var random = new Random(Guid.NewGuid().GetHashCode());
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
        
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
                sequence[i] = NextDouble(startValue, endValue);
                startValue = endValue;
                i++;
            }

            return sequence;
        }
    }
}