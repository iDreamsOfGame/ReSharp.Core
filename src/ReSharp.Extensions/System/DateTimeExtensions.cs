// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="System.DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// The <see cref="System.DateTime"/> start position.
        /// </summary>
        public static readonly DateTime StartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// Converts the value of the current <see cref="System.DateTime"/> object to a local timestamp.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <returns>
        /// The value of the current <see cref="System.DateTime"/> object expressed as a local timestamp.
        /// </returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            var timeSpan = dateTime.ToLocalTime() - StartTime.ToLocalTime();
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }

        /// <summary>
        /// Converts the value of the current <see cref="System.DateTime"/> object to a local timestamp in milliseconds.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <returns>
        /// The value of the current <see cref="System.DateTime"/> object expressed as a local timestamp in milliseconds.
        /// </returns>
        public static long ToTimestampInMilliseconds(this DateTime dateTime)
        {
            var timeSpan = dateTime.ToLocalTime() - StartTime.ToLocalTime();
            return Convert.ToInt64(timeSpan.TotalMilliseconds);
        }

        /// <summary>
        /// Converts the value of the current <see cref="System.DateTime"/> object to an UTC timestamp.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <returns>
        /// The value of the current <see cref="System.DateTime"/> object expressed as an UTC timestamp.
        /// </returns>
        public static long ToTimestampUtc(this DateTime dateTime)
        {
            var timeSpan = dateTime.ToUniversalTime() - StartTime.ToUniversalTime();
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }

        /// <summary>
        /// Converts the value of the current <see cref="System.DateTime"/> object to an UTC timestamp in milliseconds.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <returns>
        /// The value of the current <see cref="System.DateTime"/> object expressed as an UTC timestamp in milliseconds.
        /// </returns>
        public static long ToTimestampInMillisecondsUtc(this DateTime dateTime)
        {
            var timeSpan = dateTime.ToUniversalTime() - StartTime.ToUniversalTime();
            return Convert.ToInt64(timeSpan.TotalMilliseconds);
        }
    }
}