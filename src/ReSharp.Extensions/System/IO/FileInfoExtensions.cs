// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace System.IO
{
    /// <summary>
    /// Extension methods collection of <see cref="FileInfo"/>.
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Renames the file.
        /// </summary>
        /// <param name="source">The source object of FileInfo.</param>
        /// <param name="newFileName">The new file name.</param>
        public static void Rename(this FileInfo source, string newFileName)
        {
            var dirPath = source.DirectoryName;
            if (string.IsNullOrEmpty(dirPath))
                return;
            
            var destPath = Path.Combine(dirPath, newFileName);
            source.MoveTo(destPath);
        }
    }
}