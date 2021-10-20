using NUnit.Framework;
using ReSharp.Patterns.State;

namespace ReSharp.Tests.Patterns.State
{
    [TestFixture]
    public class StateMachineTests
    {
        private class TestState : IState
        {
            #region Properties

            public int Count { get; private set; }

            #endregion Properties

            #region Methods

            public void OnEnter(IState prevState)
            {
                Count++;
            }

            public void OnExecute()
            {
                Count += 2;
            }

            public void OnExit(IState nextState)
            {
                Count--;
            }

            #endregion Methods
        }
        
        #region Methods

        [Test]
        public void ChangeStateTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.ChangeState(state);
            Assert.AreSame(state, context.CurrentState);
        }

        [Test]
        public void RaiseStateChangedEventTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.StateChanged += (sender, e) => { Assert.AreSame(e.State, state); };
            context.ChangeState(state);
        }

        [Test]
        public void StateOnEnterTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.ChangeState(state);
            Assert.AreEqual(1, state.Count);
        }

        [Test]
        public void StateOnExecuteTest()
        {
            var context = new StateMachine();
            var state = new TestState();
            context.ChangeState(state);
            context.Execute();
            Assert.AreEqual(3, state.Count);
        }

        [Test]
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