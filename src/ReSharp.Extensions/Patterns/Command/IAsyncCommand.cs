// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// The interface definition for an asynchronous command.
    /// </summary>
    public interface IAsyncCommand
    {
        /// <summary>
        /// Aborts the asynchronous operation.
        /// </summary>
        void Abort();

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="executedCallback">
        /// The callback method when <see cref="IAsyncCommand" /> executed to invoke.
        /// </param>
        void Execute(Action executedCallback);
    }
}