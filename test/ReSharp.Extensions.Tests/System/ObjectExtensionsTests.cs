#pragma warning disable CS0067 // Event is never used

using System;
using NUnit.Framework;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedTypeParameter
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToAutoProperty

namespace ReSharp.Extensions.Tests
{
    public class IdInfo
    {
        internal int id;

        public int Id
        {
            get => id;
            set => id = value;
        }
    }

    public class PersonBase
    {
        public const int DefaultSex = 1;
        
        internal int sex;

        public int Sex
        {
            get => sex;
            set => sex = value;
        }

        public bool IsMale() => sex == DefaultSex;

        public void ChangeSex()
        {
            sex = sex == DefaultSex ? 0 : DefaultSex;
        }

        public void GenericMethodBase<T>() where T : IdInfo
        {
            ChangeSex();
        }
    }

    public class Person : PersonBase
    {
        public const string DefaultName = "Tester";
        
        internal int age;
        
        internal string name;
        
        internal IdInfo idInfo;

        public int Age
        {
            get => age;
            set => age = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public IdInfo IdInfo
        {
            get => idInfo;
            set => idInfo = value;
        }

        public bool IsTester() => Name.Equals(DefaultName);

        public void IncreaseAge()
        {
            Age++;
        }
        
        public void GenericMethod<T>() where T : IdInfo
        {
            IncreaseAge();
        }
    }

    public class EventTesterBase
    {
        public event EventHandler TestBaseEvent;
        
        public event EventHandler TestBaseEvent2;
    }

    public class EventTester : EventTesterBase
    {
        public const int DefaultIntValue1 = 10;

        public const int DefaultIntValue2 = 20;
        
        public event EventHandler TestEvent;

        public event EventHandler TestEvent2;

        public int CounterA { get; private set; } = DefaultIntValue1;

        public int CounterB { get; private set; } = DefaultIntValue2;

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
            this.RemoveEventHandlers(nameof(TestEvent));
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
            [Description("Value 1")]
            Value1
        }
        
        [Test]
        public void ShallowCloneTest()
        {
            var p1 = new Person
            {
                Age = 42,
                Name = Person.DefaultName,
                IdInfo = new IdInfo { Id = 6565 }
            };

            var p2 = p1.ShallowClone();
            p2.IdInfo.Id = 65;

            Assert.AreEqual(p1.IdInfo.Id, p2.IdInfo.Id);
        }

        [Test]
        public void DeepCloneTest()
        {
            var p1 = new Person
            {
                Age = 42,
                Name = Person.DefaultName,
                IdInfo = new IdInfo { Id = 6565 }
            };

            if (p1.DeepClone() is Person p2)
            {
                p2.IdInfo.Id = 65;
                Assert.AreNotEqual(p1.IdInfo.Id, p2.IdInfo.Id);
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
                Name = Person.DefaultName,
                IdInfo = new IdInfo { Id = 6565 }
            };

            var p2 = p1.DeepClone<Person>();
            p2.IdInfo.Id = 65;
            Assert.AreNotEqual(p1.IdInfo.Id, p2.IdInfo.Id);
        }

        [Test]
        public void GetCustomAttributeTest()
        {
            var enumValue = TestEnum.Value1;
            var attribute = enumValue.GetCustomAttribute<DescriptionAttribute>();
            Assert.AreEqual("Value 1", attribute.Description);
        }

        [Test]
        public void GetEventFieldInfoTest1()
        {
            var eventTester = new EventTester();
            Assert.NotNull(eventTester.GetEventFieldInfo(nameof(eventTester.TestEvent)));
        }
        
        [Test]
        public void GetEventFieldInfoTest2()
        {
            var eventTester = new EventTester();
            Assert.NotNull(eventTester.GetEventFieldInfo(nameof(eventTester.TestBaseEvent)));
        }

        [Test]
        public void GetFieldInfoTest1()
        {
            var person = new Person();
            Assert.NotNull(person.GetFieldInfo(nameof(person.name)));
        }
        
        [Test]
        public void GetFieldInfoTest2()
        {
            var person = new Person();
            Assert.NotNull(person.GetFieldInfo(nameof(person.sex)));
        }

        [Test]
        public void GetFieldValueTest1()
        {
            var person = new Person
            {
                Name = Person.DefaultName
            };
            Assert.AreEqual(Person.DefaultName, person.GetFieldValue(nameof(person.name)));
        }
        
        [Test]
        public void GetFieldValueTest2()
        {
            var person = new Person
            {
                Sex = PersonBase.DefaultSex
            };
            Assert.AreEqual(PersonBase.DefaultSex, person.GetFieldValue(nameof(person.sex)));
        }

        [Test]
        public void SetFieldValueTest1()
        {
            var person = new Person();
            person.SetFieldValue(nameof(person.name), Person.DefaultName);
            Assert.AreEqual(Person.DefaultName, person.Name);
        }

        [Test]
        public void SetFieldValueTest2()
        {
            var person = new Person();
            person.SetFieldValue(nameof(person.sex), PersonBase.DefaultSex);
            Assert.AreEqual(PersonBase.DefaultSex, person.Sex);
        }

        [Test]
        public void GetFieldValuePairsTest1()
        {
            var idInfo = new IdInfo();
            var pairs = idInfo.GetFieldValuePairs();
            Assert.AreEqual(1, pairs.Count);
        }
        
        [Test]
        public void GetFieldValuePairsTest2()
        {
            var person = new Person();
            var pairs = person.GetFieldValuePairs();
            Assert.AreEqual(4, pairs.Count);
        }

        [Test]
        public void GetPropertyInfoTest1()
        {
            var person = new Person();
            Assert.NotNull(person.GetPropertyInfo(nameof(person.Name)));
        }
        
        [Test]
        public void GetPropertyInfoTest2()
        {
            var person = new Person();
            Assert.NotNull(person.GetPropertyInfo(nameof(person.Sex)));
        }

        [Test]
        public void GetPropertyValueTest1()
        {
            var person = new Person
            {
                Name = Person.DefaultName
            };
            Assert.AreEqual(Person.DefaultName, person.GetPropertyValue(nameof(person.Name)));
        }
        
        [Test]
        public void GetPropertyValueTest2()
        {
            var person = new Person
            {
                Sex = PersonBase.DefaultSex
            };
            Assert.AreEqual(PersonBase.DefaultSex, person.GetPropertyValue(nameof(person.Sex)));
        }

        [Test]
        public void SetPropertyValueTest1()
        {
            var person = new Person();
            person.SetPropertyValue(nameof(person.Name), Person.DefaultName);
            Assert.AreEqual(Person.DefaultName, person.Name);
        }
        
        [Test]
        public void SetPropertyValueTest2()
        {
            var person = new Person();
            person.SetPropertyValue(nameof(person.Sex), PersonBase.DefaultSex);
            Assert.AreEqual(PersonBase.DefaultSex, person.Sex);
        }

        [Test]
        public void GetPropertyValuePairsTest1()
        {
            var idInfo = new IdInfo();
            var pairs = idInfo.GetPropertyValuePairs();
            Assert.AreEqual(1, pairs.Count);
        }
        
        [Test]
        public void GetPropertyValuePairsTest2()
        {
            var person = new Person();
            var pairs = person.GetPropertyValuePairs();
            Assert.AreEqual(4, pairs.Count);
        }

        [Test]
        public void GetMethodInfoTest1()
        {
            var person = new Person();
            Assert.NotNull(person.GetMethodInfo(nameof(person.IsTester)));
        }
        
        [Test]
        public void GetMethodInfoTest2()
        {
            var person = new Person();
            Assert.NotNull(person.GetMethodInfo(nameof(person.IsMale)));
        }

        [Test]
        public void HasMethodTest1()
        {
            var person = new Person();
            Assert.True(person.HasMethod(nameof(person.IsTester)));
        }
        
        [Test]
        public void HasMethodTest2()
        {
            var person = new Person();
            Assert.True(person.HasMethod(nameof(person.IsMale)));
        }

        [Test]
        public void InvokeMethodTest1()
        {
            var person = new Person
            {
                Age = 20
            };
            person.InvokeMethod(nameof(person.IncreaseAge));
            Assert.AreEqual(21, person.Age);
        }

        [Test]
        public void InvokeMethodTest2()
        {
            var person = new Person
            {
                Sex = PersonBase.DefaultSex
            };
            person.InvokeMethod(nameof(person.ChangeSex));
            Assert.AreEqual(0, person.Sex);
        }

        [Test]
        public void InvokeGenericMethodTest1()
        {
            var person = new Person
            {
                Age = 20
            };
            person.InvokeGenericMethod("GenericMethod", new[] { typeof(IdInfo) });
            Assert.AreEqual(21, person.Age);
        }

        [Test]
        public void InvokeGenericMethodTest2()
        {
            var person = new Person
            {
                Sex = PersonBase.DefaultSex
            };
            person.InvokeGenericMethod("GenericMethodBase", new[] { typeof(IdInfo) });
            Assert.AreEqual(0, person.Sex);
        }

        [Test]
        public void GetEventInfoListTest1()
        {
            var eventTesterBase = new EventTesterBase();
            Assert.AreEqual(2, eventTesterBase.GetEventInfoList().Count);
        }
        
        [Test]
        public void GetEventInfoListTest2()
        {
            var eventTester = new EventTester();
            Assert.AreEqual(4, eventTester.GetEventInfoList().Count);
        }

        [Test]
        public void RemoveEventHandlerTest()
        {
            var eventTester = new EventTester();
            eventTester.Test2();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(EventTester.DefaultIntValue1, eventTester.CounterA);
                Assert.AreEqual(EventTester.DefaultIntValue2 + 1, eventTester.CounterB);
            });
        }

        [Test]
        public void RemoveAllEventHandlersTest()
        {
            var eventTester = new EventTester();
            eventTester.Test1();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(EventTester.DefaultIntValue1, eventTester.CounterA);
                Assert.AreEqual(EventTester.DefaultIntValue2, eventTester.CounterB);
            });
        }
    }
}