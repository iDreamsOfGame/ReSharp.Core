// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace System
{
    /// <summary>
    /// Provides utility functions for object of <see cref="DateTime" />.
    /// </summary>
    public static class DateTimeUtility
    {
        #region Properties

        /// <summary>
        /// Gets a local timestamp of the <see cref="System.DateTime.Now" />.
        /// </summary>
        /// <value>The value of the <see cref="System.DateTime.Now" /> expressed as a local timestamp.</value>
        public static long NowTimestamp => DateTime.Now.ToTimestamp();

        /// <summary>
        /// Gets an UTC timestamp of the <see cref="System.DateTime.UtcNow" />.
        /// </summary>
        /// <value>The value of the <see cref="System.DateTime.UtcNow" /> expressed as an UTC timestamp.</value>
        public static long UtcNowTimestamp => DateTime.UtcNow.ToTimestampUtc();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Converts a local timestamp to a <see cref="System.DateTime" /> object.
        /// </summary>
        /// <param name="timestamp">The locale timestamp.</param>
        /// <returns>A <see cref="System.DateTime" /> object represents the local timestamp.</returns>
        public static DateTime ParseTimestamp(long timestamp) => DateTimeExtensions.StartTime.ToLocalTime().AddSeconds(timestamp);

        /// <summary>
        /// Converts an UTC timestamp to a <see cref="System.DateTime" /> object.
        /// </summary>
        /// <param name="timestamp">The UTC timestamp.</param>
        /// <returns>A <see cref="System.DateTime" /> object represents the UTC timestamp.</returns>
        public static DateTime ParseTimestampUtc(long timestamp) => DateTimeExtensions.StartTime.AddSeconds(timestamp);

        #endregion Methods
    }
}