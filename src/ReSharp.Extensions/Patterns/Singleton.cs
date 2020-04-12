// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Reflection;

namespace ReSharp.Patterns
{
    /// <summary>
    /// Abstract class for implementing singleton pattern of thread-safety by using double-check locking.
    /// </summary>
    /// <typeparam name="T">The type of the class.</typeparam>
    public abstract class Singleton<T> : IDisposable where T : class
    {
        #region Fields

        private static readonly object SyncRoot = new object();

        private static bool disposed;

        private static volatile T instance;

        #endregion Fields

        #region Destructors

        /// <summary>
        /// Finalizes an instance of the <see cref="Singleton{T}" /> class.
        /// </summary>
        ~Singleton()
        {
            Dispose(false);
        }

        #endregion Destructors

        #region Properties

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
                    Type type = typeof(T);
                    ConstructorInfo ctor = type.GetConstructor(BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);

                    if (ctor != null)
                    {
                        instance = (T)ctor.Invoke(null);
                    }
                    else
                    {
                        throw new MissingMethodException(type.FullName, "non-public Constructor");
                    }
                }

                return instance;
            }
        }

        #endregion Properties

        #region Methods

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

        #endregion Methods
    }
}