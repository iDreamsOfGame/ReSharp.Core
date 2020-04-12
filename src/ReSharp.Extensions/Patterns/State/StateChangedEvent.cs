// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;

namespace ReSharp.Patterns.State
{
    /// <summary>
    /// The event args given to listeners of the <see cref="StateMachine.StateChanged" /> event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class StateChangedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangedEventArgs" /> class with the
        /// given information.
        /// </summary>
        /// <param name="previousState">
        /// The state before the <see cref="StateMachine.StateChanged" /> event.
        /// </param>
        /// <param name="state">The state after the <see cref="StateMachine.StateChanged" /> event.</param>
        public StateChangedEventArgs(IState previousState, IState state)
            : base()
        {
            PreviousState = previousState;
            State = state;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the state before the <see cref="StateMachine.StateChanged" /> event.
        /// </summary>
        /// <value>The the previous state.</value>
        public IState PreviousState
        {
            get;
        }

        /// <summary>
        /// Gets the state after the <see cref="StateMachine.StateChanged" /> event.
        /// </summary>
        /// <value>The current state.</value>
        public IState State
        {
            get;
        }

        #endregion Properties
    }
}