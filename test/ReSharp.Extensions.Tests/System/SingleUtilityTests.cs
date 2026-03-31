using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class SingleUtilityTests
    {
        [Test]
        public void GenericParseTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            var result = SingleUtility.GenericParse("0.123");
            Assert.AreEqual(0.123f, result);
        }
        
        [Test]
        public void GenericTryParseTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            SingleUtility.GenericTryParse("0.123", out var result);
            Assert.AreEqual(0.123f, result);
        }
    }
}