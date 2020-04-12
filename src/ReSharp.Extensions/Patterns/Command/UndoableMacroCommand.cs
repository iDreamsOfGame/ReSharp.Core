// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using System.Collections.Generic;

namespace ReSharp.Patterns.Command
{
    /// <summary>
    /// A base <see cref="IUndoableMacroCommand" /> implementation to execute <see
    /// cref="IUndoableCommand" />, perform undo and redo operation.
    /// </summary>
    /// <seealso cref="IUndoableMacroCommand" />
    public class UndoableMacroCommand : IUndoableMacroCommand
    {
        #region Fields

        private readonly Stack<IUndoableCommand> redoCommandStack;
        private readonly Stack<IUndoableCommand> undoCommandStack;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoableMacroCommand" /> class.
        /// </summary>
        public UndoableMacroCommand()
        {
            undoCommandStack = new Stack<IUndoableCommand>();
            redoCommandStack = new Stack<IUndoableCommand>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="UndoableMacroCommand" /> can perform
        /// redo operation.
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="UndoableMacroCommand" /> can perform redo operation;
        /// otherwise, <c>false</c>.
        /// </value>
        public bool CanRedo => redoCommandStack.Count > 0;

        /// <summary>
        /// Gets a value indicating whether this <see cref="UndoableMacroCommand" /> can perform
        /// undo operation.
        /// </summary>
        /// <value>
        /// <c>true</c> if this <see cref="UndoableMacroCommand" /> can perform undo operation;
        /// otherwise, <c>false</c>.
        /// </value>
        public bool CanUndo => undoCommandStack.Count > 0;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Executes the specific <see cref="IUndoableCommand" />.
        /// </summary>
        /// <param name="command">The sepecified <see cref="IUndoableCommand" /> to execute.</param>
        /// <exception cref="ArgumentNullException"><c>command</c> is <c>null</c>.</exception>
        public void Execute(IUndoableCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.Execute();
            redoCommandStack.Clear();
            undoCommandStack.Push(command);
        }

        /// <summary>
        /// Performs redo operation.
        /// </summary>
        public void Redo()
        {
            if (!CanRedo)
                return;

            IUndoableCommand command = redoCommandStack.Pop();
            command.Execute();
            undoCommandStack.Push(command);
        }

        /// <summary>
        /// Performs undo operation.
        /// </summary>
        public void Undo()
        {
            if (!CanUndo)
                return;

            IUndoableCommand command = undoCommandStack.Pop();
            command.Undo();
            redoCommandStack.Push(command);
        }

        #endregion Methods
    }
}