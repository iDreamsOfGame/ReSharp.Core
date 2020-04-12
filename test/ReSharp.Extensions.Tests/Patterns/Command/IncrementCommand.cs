using ReSharp.Patterns.Command;
using System;
using System.Threading;

namespace ReSharp.Tests.Patterns.Command
{
    internal class IncrementAsyncCommand : TestAsyncCommand
    {
        #region Constructors

        public IncrementAsyncCommand(Counter counter)
            : base(counter)
        {
        }

        #endregion Constructors

        #region Methods

        public override void Execute(Action executedCallback)
        {
            base.Execute(executedCallback);
            Thread thread = new Thread(new ThreadStart(Do));
            thread.Start();
        }

        private void Do()
        {
            Thread.Sleep(500);

            lock (Counter)
            {
                Counter.Count++;
            }

            Executed();
        }

        #endregion Methods
    }

    internal class IncrementCommand : TestCommand, ICommand
    {
        #region Constructors

        public IncrementCommand(Counter counter)
            : base(counter)
        {
        }

        #endregion Constructors

        #region Methods

        public void Execute()
        {
            Counter.Count++;
        }

        #endregion Methods
    }

    internal class IncrementUndoableCommand : TestCommand, IUndoableCommand
    {
        #region Constructors

        public IncrementUndoableCommand(Counter counter)
            : base(counter)
        {
        }

        #endregion Constructors

        #region Methods

        public void Execute()
        {
            Counter.Count++;
        }

        public void Undo()
        {
            Counter.Count--;
        }

        #endregion Methods
    }
}