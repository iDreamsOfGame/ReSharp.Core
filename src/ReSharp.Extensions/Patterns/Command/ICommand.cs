// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// The interface definition for the command.
    /// </summary>
    public interface ICommand
    {
        #region Methods

        /// <summary>
        /// Executes this command.
        /// </summary>
        void Execute();

        #endregion Methods
    }
}