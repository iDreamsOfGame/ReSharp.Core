// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReSharp.Extensions
{
    /// <summary>
    /// Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of <see cref="FileStream"/> objects.
    /// </summary>
    public static class FileUtility
    {
        /// <summary>
        /// Tries to create or overwrites a file in the specified path, specifying a buffer size and options that describe how to create or overwrite the file.
        /// </summary>
        /// <param name="path">The path and name of the file to create. </param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param>
        /// <param name="options">One of the <see cref="FileOptions"/> values that describes how to create or overwrite the file. </param>
        /// <param name="fileStream">A new file with the specified buffer size. </param>
        /// <returns><c>true</c> if create or overwrites a file in the specified path successfully; <c>false</c> otherwise. </returns>
        public static bool TryCreate(string path,
            int bufferSize,
            FileOptions options,
            out FileStream fileStream)
        {
            try
            {
                fileStream = File.Create(path, bufferSize, options);
                return true;
            }
            catch (Exception)
            {
                fileStream = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to create or overwrites a file in the specified path.
        /// </summary>
        /// <param name="path">The path and name of the file to create. </param>
        /// <param name="fileStream">A <see cref="FileStream"/> that provides read/write access to the file specified in <c>path</c>.</param>
        /// <returns><c>true</c> if create or overwrites a file in the specified path successfully; <c>false</c> otherwise. </returns>
        public static bool TryCreate(string path, out FileStream fileStream)
        {
            try
            {
                fileStream = File.Create(path);
                return true;
            }
            catch (Exception)
            {
                fileStream = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to create or overwrites a file in the specified path, specifying a buffer size.
        /// </summary>
        /// <param name="path">The path and name of the file to create. </param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param>
        /// <param name="fileStream">A <see cref="FileStream"/> with the specified buffer size that provides read/write access to the file specified in <c>path</c>. </param>
        /// <returns><c>true</c> if create or overwrites a file in the specified path successfully; <c>false</c> otherwise. </returns>
        public static bool TryCreate(string path, int bufferSize, out FileStream fileStream)
        {
            try
            {
                fileStream = File.Create(path, bufferSize);
                return true;
            }
            catch (Exception)
            {
                fileStream = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to create or opens a file for writing UTF-8 encoded text. If the file already exists, its contents are overwritten.
        /// </summary>
        /// <param name="path">The file to be opened for writing. </param>
        /// <param name="streamWriter">A <see cref="StreamWriter"/> that writes to the specified file using UTF-8 encoding. </param>
        /// <returns><c>true</c> if create or opens a file for writing UTF-8 encoded text successfully; <c>false</c> otherwise. </returns>
        public static bool TryCreateText(string path, out StreamWriter streamWriter)
        {
            try
            {
                streamWriter = File.CreateText(path);
                return true;
            }
            catch (Exception)
            {
                streamWriter = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to open a <see cref="FileStream"/> on the specified path with read/write access with no sharing.
        /// </summary>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A <see cref="FileMode"/> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
        /// <param name="fileStream">A <see cref="FileStream"/> opened in the specified mode and path, with read/write access and not shared. </param>
        /// <returns><c>true</c> if open a <see cref="FileStream"/> on the specified path successfully; <c>false</c> otherwise. </returns>
        public static bool TryOpen(string path, FileMode mode, out FileStream fileStream)
        {
            try
            {
                fileStream = File.Open(path, mode);
                return true;
            }
            catch (Exception)
            {
                fileStream = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to opens a <see cref="FileStream"/> on the specified path, with the specified mode and access with no sharing.
        /// </summary>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A <see cref="FileMode"/> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
        /// <param name="access">A <see cref="FileAccess"/> value that specifies the operations that can be performed on the file. </param>
        /// <param name="fileStream">An unshared <see cref="FileStream"/> that provides access to the specified file, with the specified mode and access. </param>
        /// <returns><c>true</c> if open a <see cref="FileStream"/> on the specified path successfully; <c>false</c> otherwise. </returns>
        public static bool TryOpen(string path,
            FileMode mode,
            FileAccess access,
            out FileStream fileStream)
        {
            try
            {
                fileStream = File.Open(path, mode, access);
                return true;
            }
            catch (Exception)
            {
                fileStream = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to open a <see cref="FileStream"/> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
        /// </summary>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A <see cref="FileMode"/> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
        /// <param name="access">A <see cref="FileAccess"/> value that specifies the operations that can be performed on the file. </param>
        /// <param name="share">A <see cref="FileShare"/> value specifying the type of access other threads have to the file. </param>
        /// <param name="fileStream">A <see cref="FileStream"/> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option. </param>
        /// <returns><c>true</c> if open a <see cref="FileStream"/> on the specified path successfully; <c>false</c> otherwise. </returns>
        public static bool TryOpen(string path,
            FileMode mode,
            FileAccess access,
            FileShare share,
            out FileStream fileStream)
        {
            try
            {
                fileStream = File.Open(path, mode, access, share);
                return true;
            }
            catch (Exception)
            {
                fileStream = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to open an existing file for reading.
        /// </summary>
        /// <param name="path">The file to be opened for reading. </param>
        /// <param name="fileStream">A read-only <see cref="FileStream"/> on the specified path. </param>
        /// <returns><c>true</c> if open a read-only <see cref="FileStream"/> on the specified path successfully; <c>false</c> otherwise. </returns>
        public static bool TryOpenRead(string path, out FileStream fileStream)
        {
            try
            {
                fileStream = File.OpenRead(path);
                return true;
            }
            catch (Exception)
            {
                fileStream = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to open an existing UTF-8 encoded text file for reading.
        /// </summary>
        /// <param name="path">The file to be opened for reading. </param>
        /// <param name="streamReader">A <see cref="StreamReader"/> on the specified path. </param>
        /// <returns><c>true</c> if open an existing UTF-8 encoded text file successfully; <c>false</c> otherwise. </returns>
        public static bool TryOpenText(string path, out StreamReader streamReader)
        {
            try
            {
                streamReader = File.OpenText(path);
                return true;
            }
            catch (Exception)
            {
                streamReader = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to open an existing file or creates a new file for writing.
        /// </summary>
        /// <param name="path">The file to be opened for writing. </param>
        /// <param name="fileStream">An unshared <see cref="FileStream"/> object on the specified path with Write access. </param>
        /// <returns><c>true</c> if open an existing file or creates a new file for writing successfully; <c>false</c> otherwise. </returns>
        public static bool TryOpenWrite(string path, out FileStream fileStream)
        {
            try
            {
                fileStream = File.OpenWrite(path);
                return true;
            }
            catch (Exception)
            {
                fileStream = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to append lines to a file, and then close the file.
        /// If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to append the lines to. The file is created if it doesn't already exist. </param>
        /// <param name="contents">The lines to append to the file. </param>
        /// <returns><c>true</c> if append lines to a file successfully; <c>false</c> otherwise. </returns>
        public static bool TryAppendAllLines(string path, IEnumerable<string> contents)
        {
            try
            {
                File.AppendAllLines(path, contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Tires to append lines to a file by using a specified encoding, and then close the file.
        /// If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to append the lines to. The file is created if it doesn't already exist. </param>
        /// <param name="contents">The lines to append to the file. </param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <returns><c>true</c> if append lines to a file successfully; <c>false</c> otherwise. </returns>
        public static bool TryAppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            try
            {
                File.AppendAllLines(path, contents, encoding);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to append the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The file to append the specified string to. </param>
        /// <param name="contents">The string to append to the file. </param>
        /// <returns><c>true</c> if append the specified string to the file successfully; <c>false</c> otherwise. </returns>
        public static bool TryAppendAllText(string path, string contents)
        {
            try
            {
                File.AppendAllText(path, contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Tries to append the specified string to the file using the specified encoding, creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The file to append the specified string to. </param>
        /// <param name="contents">The string to append to the file. </param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <returns><c>true</c> if append the specified string to the file successfully; <c>false</c> otherwise. </returns>
        public static bool TryAppendAllText(string path, string contents, Encoding encoding)
        {
            try
            {
                File.AppendAllText(path, contents, encoding);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to create a <see cref="StreamWriter"/> that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist.
        /// </summary>
        /// <param name="path">The path to the file to append to. </param>
        /// <param name="streamWriter">A stream writer that appends UTF-8 encoded text to the specified file or to a new file. </param>
        /// <returns><c>true</c> if creates a <see cref="StreamWriter"/> that appends UTF-8 encoded text to an existing file successfully; <c>false</c> otherwise. </returns>
        public static bool TryAppendText(string path, out StreamWriter streamWriter)
        {
            try
            {
                streamWriter = File.AppendText(path);
                return true;
            }
            catch (Exception)
            {
                streamWriter = null;
                return false;
            }
        }
        
        /// <summary>
        /// Tries to load raw data in the file.
        /// </summary>
        /// <param name="path">The file to open for loading. </param>
        /// <param name="bytes">A byte array containing the contents of the file. </param>
        /// <returns><c>true</c> if the specified file loaded; <c>false</c> otherwise. </returns>
        public static bool TryLoad(string path, out byte[] bytes)
        {
            try
            {
                bytes = File.ReadAllBytes(path);
                return true;
            }
            catch (Exception)
            {
                bytes = null;
                return false;
            }
        }
        
        /// <summary>
        /// Tries to load all lines content in the file.
        /// </summary>
        /// <param name="path">The file to open for loading. </param>
        /// <param name="contents">A string array containing all lines of the file. </param>
        /// <returns><c>true</c> if the specified file loaded; <c>false</c> otherwise. </returns>
        public static bool TryLoad(string path, out string[] contents)
        {
            try
            {
                contents = File.ReadAllLines(path);
                return true;
            }
            catch (Exception)
            {
                contents = null;
                return false;
            }
        }
        
        /// <summary>
        /// Tries to load all lines content in the file.
        /// </summary>
        /// <param name="path">The file to open for loading. </param>
        /// <param name="encoding">The encoding applied to the contents of the file. </param>
        /// <param name="contents">A string array containing all lines of the file. </param>
        /// <returns><c>true</c> if the specified file loaded; <c>false</c> otherwise. </returns>
        public static bool TryLoad(string path, Encoding encoding, out string[] contents)
        {
            try
            {
                contents = File.ReadAllLines(path, encoding);
                return true;
            }
            catch (Exception)
            {
                contents = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to load all the text in the file.
        /// </summary>
        /// <param name="path">The file to open for loading. </param>
        /// <param name="contents">A string containing all the text in the file. </param>
        /// <returns><c>true</c> if the specified file loaded; <c>false</c> otherwise. </returns>
        public static bool TryLoad(string path, out string contents)
        {
            try
            {
                contents = File.ReadAllText(path);
                return true;
            }
            catch (Exception)
            {
                contents = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to load all the text in the file.
        /// </summary>
        /// <param name="path">The file to open for loading. </param>
        /// <param name="encoding">The encoding applied to the contents of the file. </param>
        /// <param name="contents">A string containing all the text in the file. </param>
        /// <returns><c>true</c> if the specified file loaded; <c>false</c> otherwise. </returns>
        public static bool TryLoad(string path, Encoding encoding, out string contents)
        {
            try
            {
                contents = File.ReadAllText(path, encoding);
                return true;
            }
            catch (Exception)
            {
                contents = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to create a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to save to. </param>
        /// <param name="bytes">The bytes to write to the file.</param>
        /// <returns><c>true</c> if the specified file saved with writing bytes; <c>false</c> otherwise. </returns>
        public static bool TrySave(string path, byte[] bytes)
        {
            try
            {
                File.WriteAllBytes(path, bytes);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to create a new file, writes a collection of strings to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to save to. </param>
        /// <param name="contents">The lines to write to the file. </param>
        /// <returns><c>true</c> if the specified file saved with writing lines; <c>false</c> otherwise. </returns>
        public static bool TrySave(string path, IEnumerable<string> contents)
        {
            try
            {
                File.WriteAllLines(path, contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Tries to create a new file, write the specified string array to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to save to. </param>
        /// <param name="contents">The string array to write to the file. </param>
        /// <returns><c>true</c> if the specified file saved with writing string array; <c>false</c> otherwise. </returns>
        public static bool TrySave(string path, string[] contents)
        {
            try
            {
                File.WriteAllLines(path, contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Tries to create a new file, writes a collection of strings to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to save to. </param>
        /// <param name="contents">The lines to write to the file. </param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <returns><c>true</c> if the specified file saved with writing lines; <c>false</c> otherwise. </returns>
        public static bool TrySave(string path, IEnumerable<string> contents, Encoding encoding)
        {
            try
            {
                File.WriteAllLines(path, contents, encoding);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Tries to create a new file, write the specified string array to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to save to. </param>
        /// <param name="contents">The string array to write to the file. </param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <returns><c>true</c> if the specified file saved with writing string array; <c>false</c> otherwise. </returns>
        public static bool TrySave(string path, string[] contents, Encoding encoding)
        {
            try
            {
                File.WriteAllLines(path, contents, encoding);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to create a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to save to. </param>
        /// <param name="contents">The string to write to the file. </param>
        /// <returns><c>true</c> if the specified file saved with writing contents; <c>false</c> otherwise. </returns>
        public static bool TrySave(string path, string contents)
        {
            try
            {
                File.WriteAllText(path, contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Tries to create a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to save to. </param>
        /// <param name="contents">The string to write to the file. </param>
        /// <param name="encoding">The encoding to apply to the string. </param>
        /// <returns><c>true</c> if the specified file saved with writing contents; <c>false</c> otherwise. </returns>
        public static bool TrySave(string path, string contents, Encoding encoding)
        {
            try
            {
                File.WriteAllText(path, contents, encoding);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Tries to delete the specified file without exception.
        /// </summary>
        /// <param name="path">The name of the file to be deleted. Wildcard characters are not supported. </param>
        /// <returns><c>true</c> if the specified file deleted; <c>false</c> otherwise. </returns>
        public static bool TryDelete(string path)
        {
            try
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to copy an existing file to a new file without exception. Overwriting a file of the same name is not allowed.
        /// </summary>
        /// <param name="sourceFileName">The file to copy.</param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file.</param>
        /// <returns><c>true</c> if file copied successfully; <c>false</c> otherwise. </returns>
        public static bool TryCopy(string sourceFileName, string destFileName)
        {
            try
            {
                File.Copy(sourceFileName, destFileName);
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                File.SetAttributes(destFileName, FileAttributes.Normal);
            }
            catch (Exception)
            {
                // ignored
            }

            return true;
        }

        /// <summary>
        /// Tries to copy an existing file to a new file without exception. Overwriting a file of the same name is allowed.
        /// </summary>
        /// <param name="sourceFileName">The file to copy.</param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file.</param>
        /// <param name="overwrite"><c>true</c> if the destination file can be overwritten; otherwise, <c>false</c>.</param>
        /// <returns><c>true</c> if file copied successfully; <c>false</c> otherwise. </returns>
        public static bool TryCopy(string sourceFileName, string destFileName, bool overwrite)
        {
            try
            {
                File.Copy(sourceFileName, destFileName, overwrite);
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                File.SetAttributes(destFileName, FileAttributes.Normal);
            }
            catch (Exception)
            {
                // ignored
            }

            return true;
        }

        /// <summary>
        /// Tries to move a specified file to a new location, providing the option to specify a new file name.
        /// </summary>
        /// <param name="sourceFileName">The name of the file to move. Can include a relative or absolute path. </param>
        /// <param name="destFileName">The new path and name for the file. </param>
        /// <returns><c>true</c> if a specified file moved successfully; <c>false</c> otherwise. </returns>
        public static bool TryMove(string sourceFileName, string destFileName)
        {
            try
            {
                File.Move(sourceFileName, destFileName);
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                File.SetAttributes(destFileName, FileAttributes.Normal);
            }
            catch (Exception)
            {
                // ignored
            }

            return true;
        }
    }
}