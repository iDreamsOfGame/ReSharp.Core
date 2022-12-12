// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Text;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Extension methods collection of <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The whitespace chars definitions.
        /// </summary>
        private static readonly char[] WhitespaceChars = {
            '\t', '\n', '\v', '\f', '\r', ' ', '\x0085', '\x00a0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '​', '\u2028', '\u2029', '﻿'
        };

        /// <summary>
        /// Determines whether the specified <see cref="string"/> has value.
        /// </summary>
        /// <param name="source">The <see cref="string"/> to check.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="string"/> has value; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasValue(this string source) => !string.IsNullOrEmpty(source);

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified string in this
        /// instance by KMP algorithm.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <returns>
        /// The zero-based index position of <b>value</b> if that string is found, or -1 if it is
        /// not. If <b>value</b> is <see cref="string.Empty"/>, the return value is 0.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><b>source</b> is <b>null</b>.</exception>
        public static int KmpIndexOf(this string source, string value)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (value == string.Empty)
                return 0;

            int i = 0, j = 0, result = -1;
            var nextIndexCollection = GetNextIndexCollection(value);

            while (i < source.Length && j < value.Length)
            {
                if (j == -1 || source[i] == value[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = nextIndexCollection[j];
                }
            }

            if (j >= value.Length)
                result = i - value.Length;

            return result;
        }

        /// <summary>
        /// Reverses the specified <see cref="string"/>.
        /// </summary>
        /// <param name="source">The specified <see cref="string"/>.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <b>source</b> is <b>null</b> or <see cref="string.Empty"/>.
        /// </exception>
        public static string Reverse(this string source)
        {
            if (!HasValue(source))
                throw new ArgumentNullException(nameof(source));

            var chars = source.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// Converts the specified string to camel case (just converts first character to lowercase).
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>The specified string converted to camel case.</returns>
        public static string ToCamelCase(this string source)
        {
            var chars = source.ToCharArray();
            chars[0] = char.ToLower(chars[0]);
            return new string(chars);
        }

        /// <summary>
        /// Converts the specified string to snake case.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>The specified string converted to snake case.</returns>
        public static string ToSnakeCase(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < source.Length; i++)
            {
                var ch = source[i];

                if (i == 0)
                {
                    stringBuilder.Append(char.ToLower(ch));
                }
                else
                {
                    if (char.IsUpper(ch))
                        stringBuilder.Append('_')
                            .Append(char.ToLower(ch));
                    else
                        stringBuilder.Append(ch);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts the specified string to title case (just converts first character to uppercase).
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>The specified string converted to title case.</returns>
        public static string ToTitleCase(this string source)
        {
            var chars = source.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            return new string(chars);
        }

        /// <summary>
        /// Trims all whitespace characters.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>The string with removing all whitespace characters.</returns>
        public static string TrimAll(this string source) => source.TrimAll(WhitespaceChars);

        /// <summary>
        /// Trims all characters assigned.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="trimChars">The trim characters assigned.</param>
        /// <returns>The string with removing all characters assigned.</returns>
        public static string TrimAll(this string source, params char[] trimChars)
        {
            if (trimChars == null)
                return source.TrimAll();

            var stringArr = source.Split(trimChars, StringSplitOptions.RemoveEmptyEntries);
            return string.Join("", stringArr);
        }

        private static int[] GetNextIndexCollection(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            int j = 0, k = -1;
            var nextIndexCollection = new int[value.Length];
            nextIndexCollection[0] = -1;

            while (j < value.Length - 1)
            {
                if (k == -1 || value[j] == value[k])
                {
                    j++;
                    k++;

                    if (value[j] != value[k])
                    {
                        nextIndexCollection[j] = k;
                    }
                    else
                    {
                        nextIndexCollection[j] = nextIndexCollection[k];
                    }
                }
                else
                {
                    k = nextIndexCollection[k];
                }
            }

            return nextIndexCollection;
        }
    }
}
