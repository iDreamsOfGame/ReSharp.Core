// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// The interface definition for the command can execute other <see cref="ICommand" /> s.
    /// </summary>
    public interface IMacroCommand
    {
        #region Methods

        /// <summary>
        /// Adds an <see cref="ICommand" />.
        /// </summary>
        /// <param name="subCommand">
        /// The <see cref="ICommand" /> to be executed by this <see cref="IMacroCommand" />.
        /// </param>
        void AddSubCommand(ICommand subCommand);

        #endregion Methods
    }
}