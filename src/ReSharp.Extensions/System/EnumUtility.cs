// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Provides utilities to deal with <see cref="Enum"/>.
    /// </summary>
    public static class EnumUtility
    {
        /// <summary>
        /// Converts the <see cref="string"/> to the specified <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the specified <see cref="Enum"/>.</typeparam>
        /// <param name="value">The <see cref="string"/> of the value of <see cref="Enum"/>.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <returns>The <see cref="Enum"/> value.</returns>
        public static TEnum ConvertToEnum<TEnum>(string value, bool ignoreCase = false) => (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
    }
}