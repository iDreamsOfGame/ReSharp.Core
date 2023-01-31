using System;
using NUnit.Framework;

namespace ReSharp.Extensions.Tests
{
    public class EnumDescription : Attribute
    {
        public EnumDescription(string label)
        {
            Label = label;
        }
        
        public string Label { get; }
    }
    
    public class IdInfo
    {
        public int IdNumber;
    }

    public class Person
    {
        public int Age;
        public string Name;
        public IdInfo IdInfo;
    }

    public class EventTester
    {
        public event EventHandler TestEvent;

        public event EventHandler TestEvent2;

        public int CounterA { get; private set; } = 10;

        public int CounterB { get; private set; } = 20;

        public void Test1()
        {
            TestEvent += OnTestEvent;
            TestEvent2 += OnTestEvent2;
            this.RemoveAllEventHandlers();
            TestEvent?.Invoke(this, EventArgs.Empty);
            TestEvent2?.Invoke(this, EventArgs.Empty);
        }

        public void Test2()
        {
            TestEvent += OnTestEvent;
            TestEvent2 += OnTestEvent2;
            this.RemoveEventHandlers("TestEvent");
            TestEvent?.Invoke(this, EventArgs.Empty);
            TestEvent2?.Invoke(this, EventArgs.Empty);
        }

        private void OnTestEvent(object sender, EventArgs e)
        {
            CounterA++;
        }

        private void OnTestEvent2(object sender, EventArgs e)
        {
            CounterB++;
        }
    }

    [TestFixture]
    public class ObjectExtensionsTests
    {
        public enum TestEnum
        {
            [EnumDescription("Value 1")]
            Value1
        }
        
        [Test]
        public void ShallowCloneTest()
        {
            var p1 = new Person
            {
                Age = 42,
                Name = "Sam",
                IdInfo = new IdInfo { IdNumber = 6565 }
            };

            var p2 = p1.ShallowClone();
            p2.IdInfo.IdNumber = 65;

            Assert.AreEqual(p1.IdInfo.IdNumber, p2.IdInfo.IdNumber);
        }

        [Test]
        public void DeepCloneTest()
        {
            var p1 = new Person
            {
                Age = 42,
                Name = "Sam",
                IdInfo = new IdInfo { IdNumber = 6565 }
            };

            if (p1.DeepClone() is Person p2)
            {
                p2.IdInfo.IdNumber = 65;
                Assert.AreNotEqual(p1.IdInfo.IdNumber, p2.IdInfo.IdNumber);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GenericDeepCloneTest()
        {
            var p1 = new Person
            {
                Age = 42,
                Name = "Sam",
                IdInfo = new IdInfo { IdNumber = 6565 }
            };

            var p2 = p1.DeepClone<Person>();
            p2.IdInfo.IdNumber = 65;
            Assert.AreNotEqual(p1.IdInfo.IdNumber, p2.IdInfo.IdNumber);
        }

        [Test]
        public void GetCustomAttributeTest()
        {
            var enumValue = TestEnum.Value1;
            var attribute = enumValue.GetCustomAttribute<EnumDescription>();
            Assert.AreEqual("Value 1", attribute.Label);
        }

        [Test]
        public void RemoveEventHandlerTest()
        {
            var eventTester = new EventTester();
            eventTester.Test2();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(10, eventTester.CounterA);
                Assert.AreEqual(21, eventTester.CounterB);
            });
        }

        [Test]
        public void RemoveAllEventHandlersTest()
        {
            var eventTester = new EventTester();
            eventTester.Test1();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(10, eventTester.CounterA);
                Assert.AreEqual(20, eventTester.CounterB);
            });
        }
    }
}