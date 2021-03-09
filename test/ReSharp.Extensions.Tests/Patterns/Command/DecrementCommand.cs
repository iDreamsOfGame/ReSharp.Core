using ReSharp.Patterns.Command;
using System;
using System.Threading;

namespace ReSharp.Tests.Patterns.Command
{
    internal class DecrementAsyncCommand : TestAsyncCommand
    {
        #region Constructors

        public DecrementAsyncCommand(Counter counter)
            : base(counter)
        {
            Counter = counter;
        }

        #endregion Constructors

        #region Methods

        public override void Execute(Action executedCallback)
        {
            base.Execute(executedCallback);
            new Thread(Do).Start();
        }

        private void Do()
        {
            Thread.Sleep(500);

            lock (Counter)
            {
                Counter.Count--;
            }

            Executed();
        }

        #endregion Methods
    }

    internal class DecrementCommand : TestCommand, ICommand
    {
        #region Constructors

        public DecrementCommand(Counter counter)
            : base(counter)
        {
        }

        #endregion Constructors

        #region Methods

        public void Execute()
        {
            Counter.Count--;
        }

        #endregion Methods
    }

    internal class DecrementUndoableCommand : TestCommand, IUndoableCommand
    {
        #region Constructors

        public DecrementUndoableCommand(Counter counter)
            : base(counter)
        {
        }

        #endregion Constructors

        #region Methods

        public void Execute()
        {
            Counter.Count--;
        }

        public void Undo()
        {
            Counter.Count++;
        }

        #endregion Methods
    }
}