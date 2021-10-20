using System;
using System.Threading;
using NUnit.Framework;
using ReSharp.Patterns.Command;

namespace ReSharp.Tests.Patterns.Command
{
    [TestFixture]
    public class CommandTests
    {
        internal class Counter
        {
            public Counter(int count)
            {
                Count = count;
            }

            public int Count { get; set; }
        }
        
        internal class TestAsyncCommand : AsyncCommand
        {
            public TestAsyncCommand(Counter counter)
            {
                Counter = counter;
            }

            public Counter Counter { get; protected set; }
        }

        internal class TestCommand
        {
            public TestCommand(Counter counter)
            {
                Counter = counter;
            }

            public Counter Counter { get; }
        }
        
        private class IncrementAsyncCommand : TestAsyncCommand
        {
            public IncrementAsyncCommand(Counter counter)
                : base(counter)
            {
            }

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
                    Counter.Count++;
                }

                Executed();
            }
        }

        internal class IncrementCommand : TestCommand, ICommand
        {
            public IncrementCommand(Counter counter)
                : base(counter)
            {
            }

            public void Execute()
            {
                Counter.Count++;
            }
        }

        internal class IncrementUndoableCommand : TestCommand, IUndoableCommand
        {
            public IncrementUndoableCommand(Counter counter)
                : base(counter)
            {
            }

            public void Execute()
            {
                Counter.Count++;
            }

            public void Undo()
            {
                Counter.Count--;
            }
        }
        
        internal class DecrementAsyncCommand : TestAsyncCommand
        {
            public DecrementAsyncCommand(Counter counter)
                : base(counter)
            {
                Counter = counter;
            }

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
        }

        internal class DecrementCommand : TestCommand, ICommand
        {
            public DecrementCommand(Counter counter)
                : base(counter)
            {
            }
            
            public void Execute()
            {
                Counter.Count--;
            }
        }

        internal class DecrementUndoableCommand : TestCommand, IUndoableCommand
        {
            public DecrementUndoableCommand(Counter counter)
                : base(counter)
            {
            }

            public void Execute()
            {
                Counter.Count--;
            }

            public void Undo()
            {
                Counter.Count++;
            }
        }
        
        internal class ArithmeticOperationsCommand : MacroCommand
        {
            public Counter Counter { get; private set; }

            public void Initialize(Counter counter)
            {
                Counter = counter;
                AddSubCommand(new DecrementCommand(counter));
                AddSubCommand(new IncrementCommand(counter));
                AddSubCommand(new DecrementCommand(counter));
            }
        }

        internal class AsyncArithmeticOperationsCommand : AsyncMacroCommand
        {
            public Counter Counter { get; private set; }

            public void Initialize(Counter counter)
            {
                Counter = counter;
                AddSubCommand(new DecrementAsyncCommand(counter));
                AddSubCommand(new IncrementAsyncCommand(counter));
                AddSubCommand(new DecrementAsyncCommand(counter));
            }
        }

        [Test]
        public void AsyncCommandExecute()
        {
            var counter = new Counter(0);
            new IncrementAsyncCommand(counter)
                .Execute(() =>
                {
                    Assert.AreEqual(1, counter.Count);
                });
        }

        [Test]
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

        [Test]
        public void MacroCommandExecute()
        {
            var cmd = new ArithmeticOperationsCommand();
            var counter = new Counter(-1);
            cmd.Initialize(counter);
            cmd.Execute();
            Assert.AreEqual(-2, cmd.Counter.Count);
        }

        [Test]
        public void SimpleCommandExecute()
        {
            var counter = new Counter(0);
            var cmd = new IncrementCommand(counter);
            cmd.Execute();
            Assert.AreEqual(1, cmd.Counter.Count);
        }

        [Test]
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

        [Test]
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
    }
}