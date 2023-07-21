using NUnit.Framework;

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void KmpIndexOfTest1()
        {
            const string source = "He is a good guy!";
            const string key = "guy";
            var index = source.KmpIndexOf(key);
            Assert.AreEqual(13, index);
        }

        [Test]
        public void KmpIndexOfTest2()
        {
            const string source = "He is a good guy!";
            const string key = "Guy";
            var index = source.KmpIndexOf(key, true);
            Assert.AreEqual(13, index);
        }
    }
}