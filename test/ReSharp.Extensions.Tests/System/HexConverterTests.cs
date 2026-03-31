using NUnit.Framework;
using System;

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class HexConverterTests
    {
        [Test]
        public void FromHexString_ValidHexWithoutSeparator_ConvertsCorrectly()
        {
            const string hex = "48656C6C6F";
            var bytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, bytes);
        }

        [Test]
        public void FromHexString_ValidHexWithColonSeparator_ConvertsCorrectly()
        {
            const string hex = "48:65:6C:6C:6F";
            var bytes = HexConverter.FromHexString(hex, ':');
            Assert.AreEqual(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, bytes);
        }

        [Test]
        public void FromHexString_ValidHexWithDashSeparator_ConvertsCorrectly()
        {
            const string hex = "48-65-6C-6C-6F";
            var bytes = HexConverter.FromHexString(hex, '-');
            Assert.AreEqual(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, bytes);
        }

        [Test]
        public void FromHexString_HexWith0xPrefix_ConvertsCorrectly()
        {
            const string hex = "0x48656C6C6F";
            var bytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, bytes);
        }

        [Test]
        public void FromHexString_HexWith0XPrefix_ConvertsCorrectly()
        {
            const string hex = "0X48656C6C6F";
            var bytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, bytes);
        }

        [Test]
        public void FromHexString_LowercaseHex_ConvertsCorrectly()
        {
            const string hex = "48656c6c6f";
            var bytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, bytes);
        }

        [Test]
        public void FromHexString_MixedCaseHex_ConvertsCorrectly()
        {
            const string hex = "48656C6c6F";
            var bytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, bytes);
        }

        [Test]
        public void FromHexString_EmptyString_ThrowsArgumentNullException()
        {
            const string hex = "";
            Assert.Throws<ArgumentNullException>(() => HexConverter.FromHexString(hex));
        }

        [Test]
        public void FromHexString_NullString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => HexConverter.FromHexString(null));
        }

        [Test]
        public void FromHexString_OddLengthString_ThrowsArgumentException()
        {
            const string hex = "48656C6C6";
            Assert.Throws<ArgumentException>(() => HexConverter.FromHexString(hex));
        }

        [Test]
        public void FromHexString_SingleByte_ConvertsCorrectly()
        {
            const string hex = "FF";
            var bytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(new byte[] { 0xFF }, bytes);
        }

        [Test]
        public void FromHexString_ZeroBytes_ConvertsCorrectly()
        {
            const string hex = "00000000";
            var bytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(new byte[] { 0x00, 0x00, 0x00, 0x00 }, bytes);
        }

        [Test]
        public void FromHexString_MaxByteValue_ConvertsCorrectly()
        {
            const string hex = "FFFFFFFF";
            var bytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, bytes);
        }

        [Test]
        public void ToHexString_NormalByteArray_ConvertsCorrectly()
        {
            var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            var hex = HexConverter.ToHexString(bytes);
            Assert.AreEqual("48656C6C6F", hex);
        }

        [Test]
        public void ToHexString_WithColonSeparator_ConvertsCorrectly()
        {
            var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            var hex = HexConverter.ToHexString(bytes, "X2", ':');
            Assert.AreEqual("48:65:6C:6C:6F", hex);
        }

        [Test]
        public void ToHexString_WithDashSeparator_ConvertsCorrectly()
        {
            var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            var hex = HexConverter.ToHexString(bytes, "X2", '-');
            Assert.AreEqual("48-65-6C-6C-6F", hex);
        }

        [Test]
        public void ToHexString_WithSpaceSeparator_ConvertsCorrectly()
        {
            var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            var hex = HexConverter.ToHexString(bytes, "X2", ' ');
            Assert.AreEqual("48 65 6C 6C 6F", hex);
        }

        [Test]
        public void ToHexString_LowercaseFormat_ConvertsCorrectly()
        {
            var bytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            var hex = HexConverter.ToHexString(bytes, "x2");
            Assert.AreEqual("48656c6c6f", hex);
        }

        [Test]
        public void ToHexString_EmptyArray_ReturnsEmptyString()
        {
            var bytes = new byte[] { };
            var hex = HexConverter.ToHexString(bytes);
            Assert.AreEqual("", hex);
        }

        [Test]
        public void ToHexString_SingleByte_ConvertsCorrectly()
        {
            var bytes = new byte[] { 0xFF };
            var hex = HexConverter.ToHexString(bytes);
            Assert.AreEqual("FF", hex);
        }

        [Test]
        public void ToHexString_ZeroBytes_ConvertsCorrectly()
        {
            var bytes = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            var hex = HexConverter.ToHexString(bytes);
            Assert.AreEqual("00000000", hex);
        }

        [Test]
        public void ToHexString_NullArray_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => HexConverter.ToHexString(null));
        }

        [Test]
        public void ToHexString_NoSeparator_LastByteNoSeparator()
        {
            var bytes = new byte[] { 0x48, 0x65, 0x6C };
            var hex = HexConverter.ToHexString(bytes, "X2", ':');
            Assert.AreEqual("48:65:6C", hex);
            Assert.IsFalse(hex.EndsWith(":"));
        }

        [Test]
        public void ToHexString_CustomFormat_ConvertsCorrectly()
        {
            var bytes = new byte[] { 0x0A, 0x0B, 0x0C };
            var hex = HexConverter.ToHexString(bytes, "X4");
            Assert.AreEqual("000A000B000C", hex);
        }

        [Test]
        public void RoundTrip_HexStringToBytesAndBack_MatchesOriginal()
        {
            const string originalHex = "48656C6C6F576F726C64";
            var bytes = HexConverter.FromHexString(originalHex);
            var resultHex = HexConverter.ToHexString(bytes);
            Assert.AreEqual(originalHex, resultHex);
        }

        [Test]
        public void RoundTrip_BytesToHexStringAndBack_MatchesOriginal()
        {
            var originalBytes = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x57, 0x6F, 0x72, 0x6C, 0x64 };
            var hex = HexConverter.ToHexString(originalBytes);
            var resultBytes = HexConverter.FromHexString(hex);
            Assert.AreEqual(originalBytes, resultBytes);
        }

        [Test]
        public void RoundTrip_WithSeparator_ConvertsCorrectly()
        {
            const string originalHex = "48:65:6C:6C:6F";
            var bytes = HexConverter.FromHexString(originalHex, ':');
            var resultHex = HexConverter.ToHexString(bytes, "X2", ':');
            Assert.AreEqual(originalHex, resultHex);
        }
    }
}