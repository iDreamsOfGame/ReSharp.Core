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
        /// Combines an array of strings into a path.
        /// </summary>
        /// <param name="paths">An array of parts of the path.</param>
        /// <returns>The combined paths.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// One of the strings in the array is <c>null</c>.
        /// </exception>
        public static string Combine(params string[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            string path = null;

            if (paths.Length > 0)
            {
                path = paths[0];

                if (paths.Length > 1)
                {
                    for (int i = 1, length = paths.Length; i < length; i++)
                    {
                        if (paths[i] == null)
                        {
                            throw new ArgumentNullException(string.Format("path[{0}]", i));
                        }

                        path = Path.Combine(path, paths[i]);
                    }
                }
            }

            return path;
        }

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