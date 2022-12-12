// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;

namespace ReSharp.Patterns.State
{
    /// <summary>
    /// Represents a state machine to handle the context of state transition.
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine" /> class with the given information.
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        public StateMachine(IState initialState = null)
        {
            CurrentState = initialState;
            CurrentState?.OnEnter(null);
        }

        /// <summary>
        /// Occurs when the state changed.
        /// </summary>
        public event EventHandler<StateChangedEventArgs> StateChanged;

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>The current state.</value>
        public IState CurrentState { get; private set; }

        /// <summary>
        /// Gets the previous state.
        /// </summary>
        /// <value>The previous state.</value>
        public IState PreviousState { get; private set; }

        /// <summary>
        /// Change the state of this <see cref="ReSharp.Patterns.State.StateMachine" />.
        /// </summary>
        /// <param name="state">
        /// The state value to set for this <see cref="ReSharp.Patterns.State.StateMachine" />.
        /// </param>
        /// <exception cref="ArgumentNullException">state</exception>
        public void ChangeState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            if (CurrentState == state)
                return;

            PreviousState = CurrentState;
            CurrentState = state;

            PreviousState?.OnExit(CurrentState);
            CurrentState.OnEnter(PreviousState);

            StateChanged?.Invoke(this, new StateChangedEventArgs(PreviousState, CurrentState));
        }

        /// <summary>
        /// Runs the state's execution logic.
        /// </summary>
        public virtual void Execute()
        {
            CurrentState?.OnExecute();
        }
    }
}