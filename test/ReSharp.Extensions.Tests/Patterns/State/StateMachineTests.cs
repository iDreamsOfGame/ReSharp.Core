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
            StateMachine context = new StateMachine();
            TestState state = new TestState();
            context.ChangeState(state);
            Assert.AreSame(state, context.CurrentState);
        }

        [TestMethod]
        public void StateOnEnterTest()
        {
            StateMachine context = new StateMachine();
            TestState state = new TestState();
            context.ChangeState(state);
            Assert.AreEqual(1, state.Count);
        }

        [TestMethod]
        public void StateOnExecuteTest()
        {
            StateMachine context = new StateMachine();
            TestState state = new TestState();
            context.ChangeState(state);
            context.Execute();
            Assert.AreEqual(3, state.Count);
        }

        [TestMethod]
        public void StateOnExitTest()
        {
            StateMachine context = new StateMachine();
            TestState state = new TestState();
            context.ChangeState(state);
            context.Execute();
            TestState state2 = new TestState();
            context.ChangeState(state2);
            Assert.AreEqual(2, state.Count);
        }

        [TestMethod]
        public void RaiseStateChangedEventTest()
        {
            StateMachine context = new StateMachine();
            TestState state = new TestState();
            context.StateChanged += (sender, e) => { Assert.AreSame(e.State, state); };
            context.ChangeState(state);
        }

        #endregion Methods
    }
}