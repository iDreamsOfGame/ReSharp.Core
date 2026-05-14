using System;
using NUnit.Framework;

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class RandomExtensionsTests
    {
        private Random random;

        [SetUp]
        public void SetUp()
        {
            random = new Random(42);
        }

        [Test]
        public void NextInt64_NoParameters_ReturnsNonNegativeValue()
        {
            var result = random.NextInt64();
            Assert.GreaterOrEqual(result, 0);
            Assert.Less(result, long.MaxValue);
        }

        [Test]
        public void NextInt64_WithMaxValue_ReturnsValueInRange()
        {
            const long maxValue = 1000;
            var result = random.NextInt64(maxValue);
            Assert.GreaterOrEqual(result, 0);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextInt64_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const long minValue = 500;
            const long maxValue = 1000;
            var result = random.NextInt64(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextInt64_MinValueGreaterThanMaxValue_ThrowsArgumentOutOfRangeException()
        {
            const long minValue = 1000;
            const long maxValue = 500;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextInt64(minValue, maxValue));
        }

        [Test]
        public void NextInt64_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const long value = 100;
            var result = random.NextInt64(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextUInt64_NoParameters_ReturnsValidValue()
        {
            var result = random.NextUInt64();
            Assert.GreaterOrEqual(result, 0UL);
            Assert.Less(result, ulong.MaxValue);
        }

        [Test]
        public void NextUInt64_WithMaxValue_ReturnsValueInRange()
        {
            const ulong maxValue = 1000;
            var result = random.NextUInt64(maxValue);
            Assert.GreaterOrEqual(result, 0UL);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextUInt64_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const ulong minValue = 500;
            const ulong maxValue = 1000;
            var result = random.NextUInt64(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextUInt64_MinValueGreaterThanMaxValue_ThrowsException()
        {
            const ulong minValue = 1000;
            const ulong maxValue = 500;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextUInt64(minValue, maxValue));
        }

        [Test]
        public void NextUInt64_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const ulong value = 100;
            var result = random.NextUInt64(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextSingle_NoParameters_ReturnsValueBetweenZeroAndOne()
        {
            var result = random.NextSingle();
            Assert.GreaterOrEqual(result, 0.0f);
            Assert.Less(result, 1.0f);
        }

        [Test]
        public void NextSingle_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const float minValue = 10.0f;
            const float maxValue = 20.0f;
            var result = random.NextSingle(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextSingle_MinValueGreaterThanMaxValue_ThrowsArgumentOutOfRangeException()
        {
            const float minValue = 20.0f;
            const float maxValue = 10.0f;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextSingle(minValue, maxValue));
        }

        [Test]
        public void NextSingle_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const float value = 15.5f;
            var result = random.NextSingle(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextDouble_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const double minValue = 100.0;
            const double maxValue = 200.0;
            var result = random.NextDouble(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextDouble_MinValueGreaterThanMaxValue_ThrowsArgumentOutOfRangeException()
        {
            const double minValue = 200.0;
            const double maxValue = 100.0;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextDouble(minValue, maxValue));
        }

        [Test]
        public void NextDouble_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const double value = 150.5;
            var result = random.NextDouble(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextSequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const int minValue = 0;
            const int maxValue = 100;
            const int count = 10;
            var result = random.NextSequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextSequence_ValidParameters_AllValuesInRange()
        {
            const int minValue = 0;
            const int maxValue = 100;
            const int count = 10;
            var result = random.NextSequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextSequence_MinValueGreaterThanMaxValue_ThrowsArgumentOutOfRangeException()
        {
            const int minValue = 100;
            const int maxValue = 0;
            const int count = 5;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextSequence(minValue, maxValue, count));
        }

        [Test]
        public void NextInt64Sequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const long minValue = 0;
            const long maxValue = 1000;
            const int count = 15;
            var result = random.NextInt64Sequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextInt64Sequence_ValidParameters_AllValuesInRange()
        {
            const long minValue = 0;
            const long maxValue = 1000;
            const int count = 15;
            var result = random.NextInt64Sequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextInt64Sequence_MinValueGreaterThanMaxValue_ThrowsArgumentOutOfRangeException()
        {
            const long minValue = 1000;
            const long maxValue = 0;
            const int count = 5;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextInt64Sequence(minValue, maxValue, count));
        }

        [Test]
        public void NextUInt64Sequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const ulong minValue = 0;
            const ulong maxValue = 1000;
            const int count = 15;
            var result = random.NextUInt64Sequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextUInt64Sequence_ValidParameters_AllValuesInRange()
        {
            const ulong minValue = 0;
            const ulong maxValue = 1000;
            const int count = 15;
            var result = random.NextUInt64Sequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextUInt64Sequence_MinValueGreaterThanMaxValue_ThrowsArgumentOutOfRangeException()
        {
            const ulong minValue = 1000;
            const ulong maxValue = 0;
            const int count = 5;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextUInt64Sequence(minValue, maxValue, count));
        }

        [Test]
        public void NextSingleSequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const float minValue = 0.0f;
            const float maxValue = 100.0f;
            const int count = 8;
            var result = random.NextSingleSequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextSingleSequence_ValidParameters_AllValuesInRange()
        {
            const float minValue = 0.0f;
            const float maxValue = 100.0f;
            const int count = 8;
            var result = random.NextSingleSequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextSingleSequence_MinValueGreaterThanMaxValue_ThrowsArgumentOutOfRangeException()
        {
            const float minValue = 100.0f;
            const float maxValue = 0.0f;
            const int count = 5;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextSingleSequence(minValue, maxValue, count));
        }

        [Test]
        public void NextDoubleSequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const double minValue = 0.0;
            const double maxValue = 1000.0;
            const int count = 12;
            var result = random.NextDoubleSequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextDoubleSequence_ValidParameters_AllValuesInRange()
        {
            const double minValue = 0.0;
            const double maxValue = 1000.0;
            const int count = 12;
            var result = random.NextDoubleSequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextDoubleSequence_MinValueGreaterThanMaxValue_ThrowsArgumentOutOfRangeException()
        {
            const double minValue = 1000.0;
            const double maxValue = 0.0;
            const int count = 5;
            Assert.Throws<ArgumentOutOfRangeException>(() => random.NextDoubleSequence(minValue, maxValue, count));
        }

        [Test]
        public void NextInt64_LargeRange_HandlesInfinityCorrectly()
        {
            const long minValue = long.MaxValue - 1000;
            const long maxValue = long.MaxValue;
            var result = random.NextInt64(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.LessOrEqual(result, maxValue);
        }

        [Test]
        public void NextSingle_LargeRange_HandlesInfinityCorrectly()
        {
            const float minValue = float.MaxValue / 2;
            const float maxValue = float.MaxValue;
            var result = random.NextSingle(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.LessOrEqual(result, maxValue);
        }

        [Test]
        public void NextDouble_LargeRange_HandlesInfinityCorrectly()
        {
            const double minValue = double.MaxValue / 2;
            const double maxValue = double.MaxValue;
            var result = random.NextDouble(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.LessOrEqual(result, maxValue);
        }

        [Test]
        public void NextUInt64_LargeRange_ReturnsValidValue()
        {
            const ulong minValue = ulong.MaxValue - 1000;
            const ulong maxValue = ulong.MaxValue;
            var result = random.NextUInt64(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.LessOrEqual(result, maxValue);
        }

        [Test]
        public void NextInt64_ZeroMaxValue_ReturnsZero()
        {
            var result = random.NextInt64(0);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void NextUInt64_ZeroMaxValue_ReturnsZero()
        {
            var result = random.NextUInt64(0UL);
            Assert.AreEqual(0UL, result);
        }

        [Test]
        public void NextMultipleCalls_ProducesDifferentValues()
        {
            var values = new System.Collections.Generic.HashSet<long>();
            for (int i = 0; i < 100; i++)
            {
                values.Add(random.NextInt64());
            }
            Assert.Greater(values.Count, 1);
        }

        [Test]
        public void NextUInt64MultipleCalls_ProducesDifferentValues()
        {
            var values = new System.Collections.Generic.HashSet<ulong>();
            for (int i = 0; i < 100; i++)
            {
                values.Add(random.NextUInt64());
            }
            Assert.Greater(values.Count, 1);
        }
    }
}
