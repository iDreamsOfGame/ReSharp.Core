// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// The interface definition for the command can execute other <see cref="IAsyncCommand" /> s.
    /// </summary>
    /// <seealso cref="ReSharp.Patterns.Command.IAsyncCommand" />
    public interface IAsyncMacroCommand : IAsyncCommand
    {
        #region Methods

        /// <summary>
        /// Adds an <see cref="IAsyncCommand" />.
        /// </summary>
        /// <param name="subCommand">
        /// The <see cref="IAsyncCommand" /> to be executed by this <see cref="IAsyncMacroCommand" />.
        /// </param>
        void AddSubCommand(IAsyncCommand subCommand);

        #endregion Methods
    }
}