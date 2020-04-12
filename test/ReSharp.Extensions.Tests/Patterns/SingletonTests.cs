using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReSharp.Patterns;

namespace ReSharp.Tests.Patterns
{
    [TestClass]
    public class SingletonTests
    {
        #region Methods

        [TestMethod]
        public void AreSameInstance()
        {
            var instanceA = SingletonTestClass.Instance;
            var instanceB = SingletonTestClass.Instance;
            Assert.AreSame(instanceA, instanceB);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingMethodException))]
        public void CanThrowMissingMethodException()
        {
            SingletonTestClassWithoutConstructor.Instance.Foo();
        }

        #endregion Methods

        #region Classes

        private class SingletonTestClass : Singleton<SingletonTestClass>
        {
            #region Constructors

            private SingletonTestClass()
            {
            }

            #endregion Constructors
        }

        private class SingletonTestClassWithoutConstructor : Singleton<SingletonTestClassWithoutConstructor>
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