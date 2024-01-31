using System;
using NUnit.Framework;

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        private const long ExpectedTimestamp = 1706572800;
        
        private const long ExpectedTimestampInMillisecond = 1706572800000;
        
        private static readonly DateTime TestUtcDateTime = new DateTime(2024, 1, 30, 0, 0, 0, DateTimeKind.Utc);
        
        private static readonly DateTime TestLocaDateTime = new DateTime(2024, 1, 30, 8, 0, 0, DateTimeKind.Local);
        
        [Test]
        public void ToUnixTimestampTest1()
        {
            Assert.AreEqual(ExpectedTimestamp, TestUtcDateTime.ToUnixTimestamp());
        }
        
        [Test]
        public void ToUnixTimestampTest2()
        {
            Assert.AreEqual(ExpectedTimestampInMillisecond, TestUtcDateTime.ToUnixTimestamp(true));
        }

        [Test]
        public void ToUnixTimestampTest3()
        {
            var dateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(TestLocaDateTime, TimeZoneInfo.Local.Id, "Asia/Shanghai");
            Assert.AreEqual(ExpectedTimestamp, dateTime.ToUnixTimestamp());
        }
        
        [Test]
        public void ToUnixTimestampTest4()
        {
            var dateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(TestLocaDateTime, TimeZoneInfo.Local.Id, "Asia/Shanghai");
            Assert.AreEqual(ExpectedTimestampInMillisecond, dateTime.ToUnixTimestamp(true));
        }

        [Test]
        public void ToDateTimeInTimeZoneTest()
        {
            var testDateTime = new DateTime(2024, 1, 30, 16, 0, 0, DateTimeKind.Local);
            var expected = new DateTime(2024, 1, 30, 0, 0, 0, DateTimeKind.Unspecified);
            Assert.AreEqual(expected, testDateTime.ToDateTimeInTimeZone(-8));
        }
        
        [Test]
        public void TryToUnixTimestampTest1()
        {
            TestUtcDateTime.TryToUnixTimestamp(false, out var actual);
            Assert.AreEqual(ExpectedTimestamp, actual);
        }
        
        [Test]
        public void TryToUnixTimestampTest2()
        {
            TestUtcDateTime.TryToUnixTimestamp(true, out var actual);
            Assert.AreEqual(ExpectedTimestampInMillisecond, actual);
        }
        
        [Test]
        public void TryToUnixTimestampTest3()
        {
            var dateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(TestLocaDateTime, TimeZoneInfo.Local.Id, "Asia/Shanghai");
            dateTime.TryToUnixTimestamp(false, out var actual);
            Assert.AreEqual(ExpectedTimestamp, actual);
        }
        
        [Test]
        public void TryToUnixTimestampTest4()
        {
            var dateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(TestLocaDateTime, TimeZoneInfo.Local.Id, "Asia/Shanghai");
            dateTime.TryToUnixTimestamp(true, out var actual);
            Assert.AreEqual(ExpectedTimestampInMillisecond, actual);
        }
        
        [Test]
        public void TryToUnixTimestampTest5()
        {
            var dateTime = new DateTime(0);
            var result = dateTime.TryToUnixTimestamp(false, out _);
            Assert.IsFalse(result);
        }
    }
}