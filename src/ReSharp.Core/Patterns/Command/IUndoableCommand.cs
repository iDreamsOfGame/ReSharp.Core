// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// The interface definition for the command that can perform undo operation.
    /// </summary>
    /// <seealso cref="ICommand" />
    public interface IUndoableCommand : ICommand
    {
        /// <summary>
        /// Performs undo operation.
        /// </summary>
        void Undo();
    }
}