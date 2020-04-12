// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace System.IO
{
    /// <summary>
    /// Provides methods for processing directory strings in an ideally cross-platform manner.
    /// </summary>
    public static class PathUtility
    {
        #region Methods

        /// <summary>
        /// Unifies all the path separator chars to alternate directory separator characters.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <returns>The unified path.</returns>
        public static string UnifyToAltDirectorySeparatorChar(string path)
        {
            return path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

        /// <summary>
        /// Unifies all the path alternate separator chars to directory separator characters.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        public static string UnifyToDirectorySeparatorChar(string path)
        {
            return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        #endregion Methods
    }
}