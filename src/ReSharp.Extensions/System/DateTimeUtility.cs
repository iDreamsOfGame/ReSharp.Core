// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Provides utility functions for object of <see cref="DateTime" />.
    /// </summary>
    public static class DateTimeUtility
    {
        /// <summary>
        /// The start <see cref="System.DateTime"/> of UNIX timestamp.
        /// </summary>
        public static readonly DateTime UnixTimestampStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts an Unix timestamp to an UTC <see cref="System.DateTime" /> object.
        /// </summary>
        /// <param name="unixTimestamp">The Unix timestamp. </param>
        /// <param name="inMilliseconds">Represents timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <returns>An UTC <see cref="System.DateTime" /> object represents the Unix timestamp. </returns>
        public static DateTime ParseUnixTimestamp(long unixTimestamp, bool inMilliseconds = false)
        {
#if NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETSTANDARD2_1
            var dateTimeOffset = inMilliseconds ? DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp) : DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
            return dateTimeOffset.UtcDateTime;
#else
            return inMilliseconds ? UnixTimestampStartTime.AddMilliseconds(unixTimestamp) : UnixTimestampStartTime.AddSeconds(unixTimestamp);
#endif
        }
        
        /// <summary>
        /// Tries to convert an Unix timestamp to an UTC <see cref="System.DateTime" /> object.
        /// </summary>
        /// <param name="unixTimestamp">The Unix timestamp. </param>
        /// <param name="inMilliseconds">Represents timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <param name="utcDateTime">An UTC <see cref="System.DateTime" /> object represents the Unix timestamp. </param>
        /// <returns><c>true</c> if the Unix <c>timestamp</c> parameter was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseUnixTimestamp(long unixTimestamp, bool inMilliseconds, out DateTime utcDateTime)
        {
            try
            {
                utcDateTime = ParseUnixTimestamp(unixTimestamp, inMilliseconds);
                return true;
            }
            catch (Exception)
            {
                utcDateTime = UnixTimestampStartTime;
                return false;
            }
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> represents an UTC to a <see cref="DateTime"/> represents an UTC Offset.
        /// </summary>
        /// <param name="utc">A <see cref="DateTime"/> represents an UTC. </param>
        /// <param name="offset">UTC offset. </param>
        /// <returns>A <see cref="DateTime"/> represents an UTC Offset. </returns>
        /// <exception cref="ArgumentOutOfRangeException">The <c>offset</c> must be between -12 and 12. </exception>
        public static DateTime ConvertUtcToUtcOffset(DateTime utc, int offset = 0)
        {
            if (offset < -12 || 12 < offset)
                throw new ArgumentOutOfRangeException($"The {nameof(offset)} must be between -12 and 12.");
            
            var dateTime = utc.Kind == DateTimeKind.Utc ? utc : utc.ToUniversalTime();
            dateTime = dateTime.AddHours(offset);
            return new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> represents an UTC offset to a <see cref="DateTime"/> represents an UTC.
        /// </summary>
        /// <param name="utcOffset">A <see cref="DateTime"/> represents an UTC Offset. </param>
        /// <param name="offset">UTC offset. </param>
        /// <returns>A <see cref="DateTime"/> represents an UTC. </returns>
        /// <exception cref="ArgumentOutOfRangeException">The <c>offset</c> must be between -12 and 12. </exception>
        public static DateTime ConvertUtcOffsetToUtc(DateTime utcOffset, int offset = 0)
        {
            if (offset < -12 || 12 < offset)
                throw new ArgumentOutOfRangeException($"The {nameof(offset)} must be between -12 and 12.");
            
            var dateTime = utcOffset.Kind == DateTimeKind.Unspecified ? utcOffset : DateTime.SpecifyKind(utcOffset, DateTimeKind.Unspecified);
            dateTime = dateTime.AddHours(-offset);
            return new DateTime(dateTime.Ticks, DateTimeKind.Utc);
        }
    }
}