// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// The interface definition for the macro command that can execute <see cref="IUndoableCommand"
    /// />, perform undo and redo operation.
    /// </summary>
    public interface IUndoableMacroCommand
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IUndoableMacroCommand" /> can perform
        /// redo operation.
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="IUndoableMacroCommand" /> can perform redo operation;
        /// otherwise, <c>false</c>.
        /// </value>
        bool CanRedo { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IUndoableMacroCommand" /> can perform
        /// undo operation.
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="IUndoableMacroCommand" /> can perform undo operation;
        /// otherwise, <c>false</c>.
        /// </value>
        bool CanUndo { get; }

        /// <summary>
        /// Executes the specific <see cref="IUndoableCommand" />.
        /// </summary>
        /// <param name="command">The specified <see cref="IUndoableCommand" /> to execute.</param>
        void Execute(IUndoableCommand command);

        /// <summary>
        /// Performs redo operation.
        /// </summary>
        void Redo();

        /// <summary>
        /// Performs undo operation.
        /// </summary>
        void Undo();
    }
}