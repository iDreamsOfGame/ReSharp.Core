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

        public class SingletonTestClass : Singleton<SingletonTestClass>
        {
        }
    }
}