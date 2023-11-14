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
        /// Gets a local timestamp of the <see cref="System.DateTime.Now" />.
        /// </summary>
        /// <value>The value of the <see cref="System.DateTime.Now" /> expressed as a local timestamp.</value>
        public static long NowTimestamp => DateTime.Now.ToTimestamp();

        /// <summary>
        /// Gets an UTC timestamp of the <see cref="System.DateTime.UtcNow" />.
        /// </summary>
        /// <value>The value of the <see cref="System.DateTime.UtcNow" /> expressed as an UTC timestamp.</value>
        public static long UtcNowTimestamp => DateTime.UtcNow.ToUtcTimestamp();

        /// <summary>
        /// Tries to convert a local timestamp to a <see cref="System.DateTime" /> object. 
        /// </summary>
        /// <param name="timestamp">The locale timestamp.</param>
        /// <param name="inMilliseconds">Represents the local timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <param name="dateTime">A <see cref="System.DateTime" /> object represents the local timestamp. </param>
        /// <returns><c>true</c> if the <c>timestamp</c> parameter was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseTimestamp(long timestamp, bool inMilliseconds, out DateTime dateTime)
        {
            try
            {
                dateTime = ParseTimestamp(timestamp, inMilliseconds);
                return true;
            }
            catch (Exception)
            {
                dateTime = DateTimeExtensions.UtcTimestampStartTime.ToLocalTime();
                return false;
            }
        }

        /// <summary>
        /// Converts a local timestamp to a <see cref="System.DateTime" /> object.
        /// </summary>
        /// <param name="timestamp">The locale timestamp.</param>
        /// <param name="inMilliseconds">Represents timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <returns>A <see cref="System.DateTime" /> object represents the local timestamp.</returns>
        public static DateTime ParseTimestamp(long timestamp, bool inMilliseconds = false)
        {
            var startTime = DateTimeExtensions.UtcTimestampStartTime.ToLocalTime();
            return inMilliseconds ? startTime.AddMilliseconds(timestamp) : startTime.AddSeconds(timestamp);
        }
        
        /// <summary>
        /// Tries to convert an UTC timestamp to a <see cref="System.DateTime" /> object. 
        /// </summary>
        /// <param name="timestamp">The UTC timestamp.</param>
        /// <param name="inMilliseconds">Represents the UTC timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <param name="dateTime">A <see cref="System.DateTime" /> object represents the UTC timestamp. </param>
        /// <returns><c>true</c> if the <c>timestamp</c> parameter was converted successfully; otherwise, <c>false</c>. </returns>
        public static bool TryParseUtcTimestamp(long timestamp, bool inMilliseconds, out DateTime dateTime)
        {
            try
            {
                dateTime = ParseUtcTimestamp(timestamp, inMilliseconds);
                return true;
            }
            catch (Exception)
            {
                dateTime = DateTimeExtensions.UtcTimestampStartTime.ToUniversalTime();
                return false;
            }
        }

        /// <summary>
        /// Converts an UTC timestamp to a <see cref="System.DateTime" /> object.
        /// </summary>
        /// <param name="timestamp">The UTC timestamp.</param>
        /// <param name="inMilliseconds">Represents timestamp in milliseconds or not. <c>true</c> in milliseconds; otherwise in seconds. </param>
        /// <returns>A <see cref="System.DateTime" /> object represents the UTC timestamp.</returns>
        public static DateTime ParseUtcTimestamp(long timestamp, bool inMilliseconds = false)
        {
            var startTime = DateTimeExtensions.UtcTimestampStartTime.ToUniversalTime();
            return inMilliseconds ? startTime.AddMilliseconds(timestamp) : startTime.AddSeconds(timestamp);
        }
    }
}