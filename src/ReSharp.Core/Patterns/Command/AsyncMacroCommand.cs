// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Collections.Generic;

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// A base <see cref="IAsyncMacroCommand" /> implementation to executes other <see
    /// cref="IAsyncCommand" /> s.
    /// </summary>
    /// <seealso cref="ReSharp.Patterns.Command.AsyncCommand" />
    /// <seealso cref="ReSharp.Patterns.Command.IAsyncMacroCommand" />
    public class AsyncMacroCommand : AsyncCommand, IAsyncMacroCommand
    {
        private readonly Queue<IAsyncCommand> commandQueue;

        private bool executed;

        private IAsyncCommand executingSubCommand;

        private bool isAborted;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncMacroCommand" /> class.
        /// </summary>
        public AsyncMacroCommand()
        {
            isAborted = false;
            executed = false;
            commandQueue = new Queue<IAsyncCommand>();
        }

        /// <summary>
        /// Aborts asynchronous commands execution.
        /// </summary>
        public override void Abort()
        {
            isAborted = true;
            executingSubCommand?.Abort();
        }

        /// <summary>
        /// Adds an <see cref="IAsyncCommand" />.
        /// </summary>
        /// <param name="subCommand">
        /// The <see cref="IAsyncCommand" /> to be executed by this <see cref="IAsyncMacroCommand" />.
        /// </param>
        public void AddSubCommand(IAsyncCommand subCommand)
        {
            if (!CheckAbortedOrExecuted())
                commandQueue.Enqueue(subCommand);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="executedCallback">
        /// The callback method when <see cref="AsyncCommand" /> executed to invoke.
        /// </param>
        public override void Execute(Action executedCallback)
        {
            if (CheckAbortedOrExecuted())
                return;
            
            base.Execute(executedCallback);
            ExecuteNextSubCommand();
        }

        private bool CheckAbortedOrExecuted() => isAborted || executed;

        private void ExecuteNextSubCommand()
        {
            if (CheckAbortedOrExecuted())
                return;

            if (commandQueue.Count > 0)
            {
                executingSubCommand = commandQueue.Dequeue();
                executingSubCommand.Execute(ExecuteNextSubCommand);
            }
            else
            {
                Executed();
                executed = true;
            }
        }
    }
}