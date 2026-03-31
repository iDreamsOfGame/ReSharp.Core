using System;
using NUnit.Framework;

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class Int32ExtensionsTests
    {
        [Test]
        public void GetLastDigitsTest1()
        {
            const int source = 1234;
            var lastDigits = source.GetLastDigits();
            Assert.AreEqual(4, lastDigits);
        }
        
        [Test]
        public void GetLastDigitsTest2()
        {
            const int source = 1234;
            var lastDigits = source.GetLastDigits(2);
            Assert.AreEqual(34, lastDigits);
        }
        
        [Test]
        public void GetLastDigitsTest3()
        {
            const int source = 123456789;
            var lastDigits = source.GetLastDigits(4);
            Assert.AreEqual(6789, lastDigits);
        }
        
        [Test]
        public void GetLastDigitsTest4()
        {
            const int source = 123456789;
            Assert.That(() => source.GetLastDigits(0), Throws.Exception.TypeOf<ArgumentException>());
        }
    }
}