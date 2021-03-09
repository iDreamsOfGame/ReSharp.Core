using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReSharp.Patterns.State;

namespace ReSharp.Tests.Patterns.State
{
    [TestClass]
    public class StateMachineTests
    {
        #region Methods

        [TestMethod]
        public void ChangeStateTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.ChangeState(state);
            Assert.AreSame(state, context.CurrentState);
        }

        [TestMethod]
        public void RaiseStateChangedEventTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.StateChanged += (sender, e) => { Assert.AreSame(e.State, state); };
            context.ChangeState(state);
        }

        [TestMethod]
        public void StateOnEnterTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.ChangeState(state);
            Assert.AreEqual(1, state.Count);
        }

        [TestMethod]
        public void StateOnExecuteTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.ChangeState(state);
            context.Execute();
            Assert.AreEqual(3, state.Count);
        }

        [TestMethod]
        public void StateOnExitTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.ChangeState(state);
            context.Execute();
            var state2 = new TestState();
            context.ChangeState(state2);
            Assert.AreEqual(2, state.Count);
        }

        #endregion Methods
    }
}