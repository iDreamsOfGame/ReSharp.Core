using ReSharp.Patterns.State;

namespace ReSharp.Tests.Patterns.State
{
    public class TestState : IState
    {
        #region Properties

        public int Count { get; private set; } = 0;

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
}