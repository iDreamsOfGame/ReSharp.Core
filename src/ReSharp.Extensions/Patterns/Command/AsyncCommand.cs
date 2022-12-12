// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// A base <see cref="IAsyncCommand" /> implementation to execute asynchronous operation.
    /// </summary>
    /// <seealso cref="ReSharp.Patterns.Command.IAsyncCommand" />
    public abstract class AsyncCommand : IAsyncCommand
    {
        private Action executedCallback;

        /// <summary>
        /// Aborts the asynchronous operation.
        /// </summary>
        public virtual void Abort()
        {
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="executedCallback">
        /// The callback method when <see cref="AsyncCommand" /> executed to invoke.
        /// </param>
        public virtual void Execute(Action executedCallback)
        {
            this.executedCallback = executedCallback;
        }

        /// <summary>
        /// The <see cref="AsyncCommand" /> executed.
        /// </summary>
        protected void Executed()
        {
            executedCallback?.Invoke();
        }
    }
}