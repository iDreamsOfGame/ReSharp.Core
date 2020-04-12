using ReSharp.Patterns.Command;

namespace ReSharp.Tests.Patterns.Command
{
    internal class TestAsyncCommand : AsyncCommand
    {
        #region Constructors

        public TestAsyncCommand(Counter counter)
        {
            Counter = counter;
        }

        #endregion Constructors

        #region Properties

        public Counter Counter
        {
            get;
            protected set;
        }

        #endregion Properties
    }

    internal class TestCommand
    {
        #region Constructors

        public TestCommand(Counter counter)
        {
            Counter = counter;
        }

        #endregion Constructors

        #region Properties

        public Counter Counter
        {
            get;
            protected set;
        }

        #endregion Properties
    }
}