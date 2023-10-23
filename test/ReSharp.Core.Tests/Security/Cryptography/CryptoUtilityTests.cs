using NUnit.Framework;
using ReSharp.Security.Cryptography;

namespace ReSharp.Tests.Security.Cryptography
{
    [TestFixture]
    public class CryptoUtilityTests
    {
        [Test]
        public void Md5EncryptTest1()
        {
            const string expected = "ffc5774100f87fe2";
            var actual = CryptoUtility.Md5Encrypt("1234abcd");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Md5EncryptTest2()
        {
            const string expected = "FFC5774100F87FE2";
            var actual = CryptoUtility.Md5Encrypt("1234abcd", null, false);
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Md5HashEncryptTest1()
        {
            const string expected = "ef73781effc5774100f87fe2f437a435";
            var actual = CryptoUtility.Md5HashEncrypt("1234abcd");
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Md5EncryptToBase64Test()
        {
            const string expected = "/8V3QQD4f+I=";
            var actual = CryptoUtility.Md5EncryptToBase64("1234abcd");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Md5HashEncryptTest2()
        {
            const string expected = "EF73781EFFC5774100F87FE2F437A435";
            var actual = CryptoUtility.Md5HashEncrypt("1234abcd", null, false);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Md5HashEncryptToBase64Test()
        {
            const string expected = "73N4Hv/Fd0EA+H/i9DekNQ==";
            var actual = CryptoUtility.Md5HashEncryptToBase64("1234abcd");
            Assert.AreEqual(expected, actual);
        }
    }
}