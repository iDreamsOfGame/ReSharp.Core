// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

namespace ReSharp.Patterns.State
{
    /// <summary>
    /// The interface definition for the state class.
    /// </summary>
    public interface IState
    {
        #region Methods

        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        /// <param name="prevState">The previous state.</param>
        void OnEnter(IState prevState);

        /// <summary>
        /// Called when the state is executing.
        /// </summary>
        void OnExecute();

        /// <summary>
        /// Called when the active state is exited.
        /// </summary>
        /// <param name="nextState">The next state.</param>
        void OnExit(IState nextState);

        #endregion Methods
    }
}