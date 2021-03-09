using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReSharp.Patterns.Command;

namespace ReSharp.Tests.Patterns.Command
{
    [TestClass]
    public class CommandTests
    {
        #region Methods

        [TestMethod]
        public void AsyncCommandExecute()
        {
            var counter = new Counter(0);
            var cmd = new IncrementAsyncCommand(counter);
            cmd.Execute(() =>
            {
                Assert.AreEqual(1, cmd.Counter.Count);
            });
        }

        [TestMethod]
        public void AsyncMacroCommandExecute()
        {
            var cmd = new AsyncArithmeticOperationsCommand();
            var counter = new Counter(-1);
            cmd.Initialize(counter);
            cmd.Execute(() =>
            {
                Assert.AreEqual(-2, cmd.Counter.Count);
            });
        }

        [TestMethod]
        public void MacroCommandExecute()
        {
            var cmd = new ArithmeticOperationsCommand();
            var counter = new Counter(-1);
            cmd.Initialize(counter);
            cmd.Execute();
            Assert.AreEqual(-2, cmd.Counter.Count);
        }

        [TestMethod]
        public void SimpleCommandExecute()
        {
            var counter = new Counter(0);
            var cmd = new IncrementCommand(counter);
            cmd.Execute();
            Assert.AreEqual(1, cmd.Counter.Count);
        }

        [TestMethod]
        public void UndoableMacroCommandRedo()
        {
            var counter = new Counter(0);
            var macroCommand = new UndoableMacroCommand();
            IUndoableCommand cmd = new DecrementUndoableCommand(counter);
            macroCommand.Execute(cmd);
            cmd = new DecrementUndoableCommand(counter);
            macroCommand.Execute(cmd);
            cmd = new IncrementUndoableCommand(counter);
            macroCommand.Execute(cmd);
            macroCommand.Undo();
            macroCommand.Redo();
            Assert.AreEqual(-1, counter.Count);
        }

        [TestMethod]
        public void UndoableMacroCommandUndo()
        {
            var counter = new Counter(0);
            var macroCommand = new UndoableMacroCommand();
            IUndoableCommand cmd = new DecrementUndoableCommand(counter);
            macroCommand.Execute(cmd);
            cmd = new DecrementUndoableCommand(counter);
            macroCommand.Execute(cmd);
            cmd = new IncrementUndoableCommand(counter);
            macroCommand.Execute(cmd);
            macroCommand.Undo();
            Assert.AreEqual(-2, counter.Count);
        }

        #endregion Methods

        #region Classes

        private class ArithmeticOperationsCommand : MacroCommand
        {
            #region Properties

            public Counter Counter
            {
                get;
                private set;
            }

            #endregion Properties

            #region Methods

            public void Initialize(Counter counter)
            {
                Counter = counter;
                AddSubCommand(new DecrementCommand(counter));
                AddSubCommand(new IncrementCommand(counter));
                AddSubCommand(new DecrementCommand(counter));
            }

            #endregion Methods
        }

        private class AsyncArithmeticOperationsCommand : AsyncMacroCommand
        {
            #region Properties

            public Counter Counter
            {
                get;
                private set;
            }

            #endregion Properties

            #region Methods

            public void Initialize(Counter counter)
            {
                Counter = counter;
                AddSubCommand(new DecrementAsyncCommand(counter));
                AddSubCommand(new IncrementAsyncCommand(counter));
                AddSubCommand(new DecrementAsyncCommand(counter));
            }

            #endregion Methods
        }

        #endregion Classes
    }
}