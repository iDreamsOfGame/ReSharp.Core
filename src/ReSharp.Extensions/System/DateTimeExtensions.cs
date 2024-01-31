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
        /// Converts the value of the <see cref="System.DateTime"/> object to a <see cref="System.DateTime"/> in specific time zone.
        /// </summary>
        /// <param name="dateTime">The <see cref="System.DateTime"/> object to be converted. </param>
        /// <param name="timeZone">The specific time zone which the result <see cref="System.DateTime"/> object is in. </param>
        /// <returns>A <see cref="System.DateTime"/> in specific time zone. </returns>
        public static DateTime ToDateTimeInTimeZone(this DateTime dateTime, int timeZone)
        {
            var utcDateTime = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
            return new DateTimeOffset(utcDateTime).ToOffset(TimeSpan.FromHours(timeZone)).DateTime;
        }
        
        /// <summary>
        /// Converts the value of the <see cref="System.DateTime"/> object to an Unix timestamp.
        /// </summary>
        /// <param name="dateTime">The <see cref="System.DateTime"/> object to be converted. </param>
        /// <param name="inMilliseconds">Converts to Unix timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <returns>The value of the <see cref="System.DateTime"/> object expressed as an Unix timestamp. </returns>
        public static long ToUnixTimestamp(this DateTime dateTime, bool inMilliseconds = false)
        {
            var utcDateTime = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
            var dateTimeOffset = new DateTimeOffset(utcDateTime);
            if (dateTimeOffset.UtcDateTime < DateTimeUtility.UnixTimestampStartTime)
                throw new ArgumentException("dateTime can not less than 1970-01-01T00:00:00Z", nameof(dateTime));
            
#if NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1
            return inMilliseconds ? dateTimeOffset.ToUnixTimeMilliseconds() : dateTimeOffset.ToUnixTimeSeconds();
#else
            return inMilliseconds ? ToUnixTimeMilliseconds(dateTimeOffset) : ToUnixTimeSeconds(dateTimeOffset);
#endif
        }
        
        /// <summary>
        /// Tries to convert the value of the <see cref="System.DateTime"/> object to an Unix timestamp.
        /// </summary>
        /// <param name="dateTime">The <see cref="System.DateTime"/> object to be converted. </param>
        /// <param name="inMilliseconds">Converts to Unix timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <param name="timestamp">The value of the <see cref="System.DateTime"/> object expressed as an Unix timestamp. </param>
        /// <returns><c>true</c> if the <see cref="System.DateTime"/> was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryToUnixTimestamp(this DateTime dateTime, bool inMilliseconds, out long timestamp)
        {
            try
            {
                timestamp = dateTime.ToUnixTimestamp(inMilliseconds);
                return true;
            }
            catch (Exception)
            {
                timestamp = 0L;
                return false;
            }
        }

        private static long ToUnixTimeSeconds(DateTimeOffset dateTimeOffset) =>
            (long)(dateTimeOffset.UtcDateTime - DateTimeUtility.UnixTimestampStartTime).TotalSeconds;

        private static long ToUnixTimeMilliseconds(DateTimeOffset dateTimeOffset) =>
            (long)(dateTimeOffset.UtcDateTime - DateTimeUtility.UnixTimestampStartTime).TotalMilliseconds;
    }
}