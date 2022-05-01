// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Collections.Generic;

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// A base <see cref="IMacroCommand" /> implementation to executes other <see cref="ICommand" /> s.
    /// </summary>
    /// <seealso cref="ICommand" />
    /// <seealso cref="IMacroCommand" />
    public class MacroCommand : ICommand, IMacroCommand
    {
        #region Fields

        private readonly Queue<ICommand> commandQueue;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MacroCommand" /> class.
        /// </summary>
        public MacroCommand()
        {
            commandQueue = new Queue<ICommand>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds sub command.
        /// </summary>
        /// <param name="subCommand">The sub command.</param>
        public void AddSubCommand(ICommand subCommand)
        {
            commandQueue.Enqueue(subCommand);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        public void Execute()
        {
            while (commandQueue.Count > 0)
            {
                var subCommand = commandQueue.Dequeue();
                subCommand.Execute();
            }
        }

        #endregion Methods
    }
}