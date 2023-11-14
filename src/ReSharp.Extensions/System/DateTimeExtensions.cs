// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="System.DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// The start <see cref="System.DateTime"/> of UTC timestamp.
        /// </summary>
        public static readonly DateTime UtcTimestampStartTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// Tries to convert the value of the current <see cref="System.DateTime"/> object to a local timestamp.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <param name="inMilliseconds">Converts to timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <param name="timestamp">The value of the current <see cref="System.DateTime"/> object expressed as a local timestamp. </param>
        /// <returns><c>true</c> if the <see cref="System.DateTime"/> was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryToTimestamp(this DateTime dateTime, bool inMilliseconds, out long timestamp)
        {
            try
            {
                timestamp = dateTime.ToTimestamp(inMilliseconds);
                return true;
            }
            catch (Exception)
            {
                timestamp = 0L;
                return false;
            }
        }

        /// <summary>
        /// Converts the value of the current <see cref="System.DateTime"/> object to a local timestamp.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <param name="inMilliseconds">Converts to timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <returns>
        /// The value of the current <see cref="System.DateTime"/> object expressed as a local timestamp.
        /// </returns>
        public static long ToTimestamp(this DateTime dateTime, bool inMilliseconds = false)
        {
            var timeSpan = dateTime.ToLocalTime() - UtcTimestampStartTime.ToLocalTime();
            return Convert.ToInt64(inMilliseconds ? timeSpan.TotalMilliseconds : timeSpan.TotalSeconds);
        }

        /// <summary>
        /// Tries to convert the value of the current <see cref="System.DateTime"/> object to an UTC timestamp.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <param name="inMilliseconds">Converts to timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <param name="timestamp">The value of the current <see cref="System.DateTime"/> object expressed as a local timestamp. </param>
        /// <returns><c>true</c> if the <see cref="System.DateTime"/> was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryToUtcTimestamp(this DateTime dateTime, bool inMilliseconds, out long timestamp)
        {
            try
            {
                timestamp = dateTime.ToUtcTimestamp(inMilliseconds);
                return true;
            }
            catch (Exception)
            {
                timestamp = 0L;
                return false;
            }
        }

        /// <summary>
        /// Converts the value of the current <see cref="System.DateTime"/> object to an UTC timestamp.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <param name="inMilliseconds">Converts to timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <returns>
        /// The value of the current <see cref="System.DateTime"/> object expressed as an UTC timestamp.
        /// </returns>
        public static long ToUtcTimestamp(this DateTime dateTime, bool inMilliseconds = false)
        {
            var timeSpan = dateTime.ToUniversalTime() - UtcTimestampStartTime;
            return Convert.ToInt64(inMilliseconds ? timeSpan.TotalMilliseconds : timeSpan.TotalSeconds);
        }
    }
}