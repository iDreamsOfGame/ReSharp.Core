// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Reflection;

// ReSharper disable StaticMemberInGenericType

namespace ReSharp.Patterns
{
    /// <summary>
    /// Abstract class for implementing singleton pattern of thread-safety by using double-check locking.
    /// </summary>
    /// <typeparam name="T">The type of the class.</typeparam>
    public abstract class Singleton<T> : IDisposable where T : class
    {
        private static readonly object SyncRoot = new object();

        private static bool disposed;

        private static volatile T instance;

        /// <summary>
        /// Finalizes an instance of the <see cref="Singleton{T}" /> class.
        /// </summary>
        ~Singleton()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the static instance.
        /// </summary>
        /// <value>The static instance.</value>
        public static T Instance
        {
            get
            {
                if (disposed)
                {
                    return null;
                }

                if (instance != null)
                    return instance;

                lock (SyncRoot)
                {
                    if (instance != null)
                        return instance;
                    var type = typeof(T);
                    
                    // Try to find non-public constructor.
                    var privateCtor = type.GetConstructor(
                        BindingFlags.Instance | BindingFlags.NonPublic,
                        null,
                        Type.EmptyTypes,
                        null);

                    T newInstance;
                    
                    if (privateCtor != null)
                    {
                        newInstance = (T)privateCtor.Invoke(null);
                    }
                    else
                    {
                        // Try to find  public constructor.
                        var publicCtor = type.GetConstructor(Type.EmptyTypes);
                        if (publicCtor != null)
                        {
                            newInstance = (T)publicCtor.Invoke(null);
                        }
                        else
                        {
                            throw new MissingMethodException(type.FullName, "Private constructor or public constructor without parameters.");
                        }
                    }

                    instance = newInstance;
                }

                return instance;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            instance = null;
            disposed = true;
        }
    }
}