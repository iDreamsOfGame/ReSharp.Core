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
        public void ToUtcDateTimeTest1()
        {
            Assert.AreEqual(TargetUtcDateTime, DateTimeUtility.ToUtcDateTime(Timestamp));
        }
        
        [Test]
        public void ToUtcDateTimeTest2()
        {
            Assert.AreEqual(TargetUtcDateTime, DateTimeUtility.ToUtcDateTime(TimestampInMillisecond, true));
        }

        [Test]
        public void TryToUtcDateTimeTest1()
        {
            DateTimeUtility.TryToUtcDateTime(Timestamp, false, out var actual);
            Assert.AreEqual(TargetUtcDateTime, actual);
        }
        
        [Test]
        public void TryToUtcDateTimeTest2()
        {
            DateTimeUtility.TryToUtcDateTime(TimestampInMillisecond, true, out var actual);
            Assert.AreEqual(TargetUtcDateTime, actual);
        }

        [Test]
        public void TryToUtcDateTimeTest3()
        {
            var result = DateTimeUtility.TryToUtcDateTime(long.MaxValue, false, out _);
            Assert.IsFalse(result);
        }
    }
}