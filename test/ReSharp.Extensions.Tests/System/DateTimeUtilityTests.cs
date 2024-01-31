using System;
using NUnit.Framework;

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class DateTimeUtilityTests
    {
        private const long Timestamp = 1706572800;
        
        private const long TimestampInMillisecond = 1706572800000;
        
        private static readonly DateTime TargetUtcDateTime = new DateTime(2024, 1, 30, 0, 0, 0, DateTimeKind.Utc);
        
        [Test]
        public void ParseUnixTimestampTest1()
        {
            Assert.AreEqual(TargetUtcDateTime, DateTimeUtility.ParseUnixTimestamp(Timestamp));
        }
        
        [Test]
        public void ParseUnixTimestampTest2()
        {
            Assert.AreEqual(TargetUtcDateTime, DateTimeUtility.ParseUnixTimestamp(TimestampInMillisecond, true));
        }

        [Test]
        public void TryParseUnixTimestampTest1()
        {
            DateTimeUtility.TryParseUnixTimestamp(Timestamp, false, out var actual);
            Assert.AreEqual(TargetUtcDateTime, actual);
        }
        
        [Test]
        public void TryParseUnixTimestampTest2()
        {
            DateTimeUtility.TryParseUnixTimestamp(TimestampInMillisecond, true, out var actual);
            Assert.AreEqual(TargetUtcDateTime, actual);
        }

        [Test]
        public void TryParseUnixTimestampTest3()
        {
            var result = DateTimeUtility.TryParseUnixTimestamp(long.MaxValue, false, out _);
            Assert.IsFalse(result);
        }

        [Test]
        public void ConvertUtcToUtcOffsetTest()
        {
            var testDateTime = new DateTime(2024, 1, 30, 16, 0, 0, DateTimeKind.Local);
            var expected = new DateTime(2024, 1, 30, 0, 0, 0, DateTimeKind.Unspecified);
            Assert.AreEqual(expected, DateTimeUtility.ConvertUtcToUtcOffset(testDateTime, -8));
        }

        [Test]
        public void ConvertUtcOffsetToUtcTest()
        {
            var testDateTime = new DateTime(2024, 1, 30, 0, 0, 0, DateTimeKind.Unspecified);
            var expected = new DateTime(2024, 1, 30, 8, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(expected, DateTimeUtility.ConvertUtcOffsetToUtc(testDateTime, -8));
        }
    }
}