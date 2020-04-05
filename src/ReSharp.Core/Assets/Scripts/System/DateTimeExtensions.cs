// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="System.DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Fields

        internal static readonly DateTime StartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        #endregion Fields

        #region Methods

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
        /// Converts the value of the current <see cref="System.DateTime"/> object to an UTC timestamp.
        /// </summary>
        /// <param name="dateTime">The current <see cref="System.DateTime"/> object.</param>
        /// <returns>
        /// The value of the current <see cref="System.DateTime"/> object expressed as an UTC timestamp.
        /// </returns>
        public static long ToTimestampUtc(this DateTime dateTime)
        {
            var timeSpan = dateTime.ToUniversalTime() - StartTime;
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }

        #endregion Methods
    }
}