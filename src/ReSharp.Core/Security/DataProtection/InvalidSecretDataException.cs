// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace ReSharp.Security.DataProtection
{
    /// <summary>
    /// The exception that is thrown when the value in secret data has been modified.
    /// </summary>
    /// <seealso cref="Exception"/>
    public class InvalidSecretDataException : Exception
    {
        #region Fields

        /// <summary>
        /// The error message.
        /// </summary>
        private const string ErrorMessage = "The secret data is invalid! result={0}, check={1}, key={2}, checkKey={3}";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSecretDataException"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="check">The check.</param>
        /// <param name="key">The key.</param>
        /// <param name="checkKey">The check key.</param>
        public InvalidSecretDataException(int result, int check, int key, int checkKey)
            : base(string.Format(ErrorMessage, result, check, key, checkKey))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSecretDataException"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="check">The check.</param>
        /// <param name="key">The key.</param>
        /// <param name="checkKey">The check key.</param>
        public InvalidSecretDataException(long result, long check, long key, long checkKey)
            : base(string.Format(ErrorMessage, result, check, key, checkKey))
        {
        }

        #endregion Constructors
    }
}