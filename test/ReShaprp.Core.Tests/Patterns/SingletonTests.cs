using System;
using NUnit.Framework;
using ReSharp.Patterns;

namespace ReSharp.Tests.Patterns
{
    [TestFixture]
    public class SingletonTests
    {
        #region Methods

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

        #endregion Methods

        #region Classes

        public class SingletonTestClass : Singleton<SingletonTestClass>
        {
            #region Constructors

            private SingletonTestClass()
            {
            }

            #endregion Constructors
        }

        public class SingletonTestClassWithoutConstructor : Singleton<SingletonTestClassWithoutConstructor>
        {
            #region Methods

            public void Foo()
            {
            }

            #endregion Methods
        }

        #endregion Classes
    }
}