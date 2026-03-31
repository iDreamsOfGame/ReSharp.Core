using System;
using NUnit.Framework;
using ReSharp.Patterns;

namespace ReSharp.Tests.Patterns
{
    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void AreSameInstance()
        {
            var instanceA = SingletonTestClass.Instance;
            var instanceB = SingletonTestClass.Instance;
            Assert.AreSame(instanceA, instanceB);
        }

        [Test]
        public void CanThrowMissingMethodException()
        {
            try
            {
                SingletonTestClassWithoutConstructor.Instance.Foo();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is MissingMethodException);
                return;
            }

            Assert.Fail();
        }

        public class SingletonTestClass : Singleton<SingletonTestClass>
        {
            private SingletonTestClass()
            {
            }
        }

        public class SingletonTestClassWithoutConstructor : Singleton<SingletonTestClassWithoutConstructor>
        {
            public void Foo()
            {
            }
        }
    }
}