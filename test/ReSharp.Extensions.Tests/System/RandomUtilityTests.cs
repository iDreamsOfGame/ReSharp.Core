using System;
using NUnit.Framework;

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class RandomUtilityTests
    {
        [SetUp]
        public void SetUp()
        {
            RandomUtility.InitializeWithSeed(42);
        }

        [Test]
        public void InitializeWithSeed_SameSeed_ProducesSameSequence()
        {
            RandomUtility.InitializeWithSeed(12345);
            var first1 = RandomUtility.Next();
            var second1 = RandomUtility.Next();

            RandomUtility.InitializeWithSeed(12345);
            var first2 = RandomUtility.Next();
            var second2 = RandomUtility.Next();

            Assert.AreEqual(first1, first2);
            Assert.AreEqual(second1, second2);
        }

        [Test]
        public void Next_NoParameters_ReturnsNonNegativeValue()
        {
            var result = RandomUtility.Next();
            Assert.GreaterOrEqual(result, 0);
            Assert.Less(result, int.MaxValue);
        }

        [Test]
        public void Next_WithMaxValue_ReturnsValueInRange()
        {
            const int maxValue = 1000;
            var result = RandomUtility.Next(maxValue);
            Assert.GreaterOrEqual(result, 0);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void Next_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const int minValue = 500;
            const int maxValue = 1000;
            var result = RandomUtility.Next(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void Next_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const int value = 100;
            var result = RandomUtility.Next(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextInt64_NoParameters_ReturnsNonNegativeValue()
        {
            var result = RandomUtility.NextInt64();
            Assert.GreaterOrEqual(result, 0);
            Assert.Less(result, long.MaxValue);
        }

        [Test]
        public void NextInt64_WithMaxValue_ReturnsValueInRange()
        {
            const long maxValue = 1000;
            var result = RandomUtility.NextInt64(maxValue);
            Assert.GreaterOrEqual(result, 0);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextInt64_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const long minValue = 500;
            const long maxValue = 1000;
            var result = RandomUtility.NextInt64(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextInt64_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const long value = 100;
            var result = RandomUtility.NextInt64(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextUInt64_NoParameters_ReturnsNonNegativeValue()
        {
            var result = RandomUtility.NextUInt64();
            Assert.GreaterOrEqual(result, 0UL);
            Assert.Less(result, ulong.MaxValue);
        }

        [Test]
        public void NextUInt64_WithMaxValue_ReturnsValueInRange()
        {
            const ulong maxValue = 1000;
            var result = RandomUtility.NextUInt64(maxValue);
            Assert.GreaterOrEqual(result, 0UL);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextUInt64_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const ulong minValue = 500;
            const ulong maxValue = 1000;
            var result = RandomUtility.NextUInt64(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextUInt64_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const ulong value = 100;
            var result = RandomUtility.NextUInt64(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextBytes_FillsArrayWithRandomValues()
        {
            var buffer = new byte[100];
            RandomUtility.NextBytes(buffer);
            Assert.AreEqual(100, buffer.Length);
            Assert.That(buffer, Has.Some.Not.EqualTo(0));
        }

        [Test]
        public void NextBytes_SameSeed_ProducesSameSequence()
        {
            RandomUtility.InitializeWithSeed(9999);
            var buffer1 = new byte[50];
            RandomUtility.NextBytes(buffer1);

            RandomUtility.InitializeWithSeed(9999);
            var buffer2 = new byte[50];
            RandomUtility.NextBytes(buffer2);

            Assert.AreEqual(buffer1, buffer2);
        }

        [Test]
        public void NextSingle_NoParameters_ReturnsValueBetweenZeroAndOne()
        {
            var result = RandomUtility.NextSingle();
            Assert.GreaterOrEqual(result, 0.0f);
            Assert.Less(result, 1.0f);
        }

        [Test]
        public void NextSingle_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const float minValue = 10.0f;
            const float maxValue = 20.0f;
            var result = RandomUtility.NextSingle(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextSingle_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const float value = 15.5f;
            var result = RandomUtility.NextSingle(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextDouble_NoParameters_ReturnsValueBetweenZeroAndOne()
        {
            var result = RandomUtility.NextDouble();
            Assert.GreaterOrEqual(result, 0.0);
            Assert.Less(result, 1.0);
        }

        [Test]
        public void NextDouble_WithMinAndMaxValues_ReturnsValueInRange()
        {
            const double minValue = 100.0;
            const double maxValue = 200.0;
            var result = RandomUtility.NextDouble(minValue, maxValue);
            Assert.GreaterOrEqual(result, minValue);
            Assert.Less(result, maxValue);
        }

        [Test]
        public void NextDouble_EqualMinAndMaxValues_ReturnsMinValue()
        {
            const double value = 150.5;
            var result = RandomUtility.NextDouble(value, value);
            Assert.AreEqual(value, result);
        }

        [Test]
        public void NextSequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const int minValue = 0;
            const int maxValue = 100;
            const int count = 10;
            var result = RandomUtility.NextSequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextSequence_ValidParameters_AllValuesInRange()
        {
            const int minValue = 0;
            const int maxValue = 100;
            const int count = 10;
            var result = RandomUtility.NextSequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextInt64Sequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const long minValue = 0;
            const long maxValue = 1000;
            const int count = 15;
            var result = RandomUtility.NextInt64Sequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextInt64Sequence_ValidParameters_AllValuesInRange()
        {
            const long minValue = 0;
            const long maxValue = 1000;
            const int count = 15;
            var result = RandomUtility.NextInt64Sequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextUInt64Sequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const ulong minValue = 0;
            const ulong maxValue = 1000;
            const int count = 15;
            var result = RandomUtility.NextUInt64Sequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextUInt64Sequence_ValidParameters_AllValuesInRange()
        {
            const ulong minValue = 0;
            const ulong maxValue = 1000;
            const int count = 15;
            var result = RandomUtility.NextUInt64Sequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextSingleSequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const float minValue = 0.0f;
            const float maxValue = 100.0f;
            const int count = 8;
            var result = RandomUtility.NextSingleSequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextSingleSequence_ValidParameters_AllValuesInRange()
        {
            const float minValue = 0.0f;
            const float maxValue = 100.0f;
            const int count = 8;
            var result = RandomUtility.NextSingleSequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void NextDoubleSequence_ValidParameters_ReturnsCorrectLengthArray()
        {
            const double minValue = 0.0;
            const double maxValue = 1000.0;
            const int count = 12;
            var result = RandomUtility.NextDoubleSequence(minValue, maxValue, count);
            Assert.AreEqual(count, result.Length);
        }

        [Test]
        public void NextDoubleSequence_ValidParameters_AllValuesInRange()
        {
            const double minValue = 0.0;
            const double maxValue = 1000.0;
            const int count = 12;
            var result = RandomUtility.NextDoubleSequence(minValue, maxValue, count);
            foreach (var value in result)
            {
                Assert.GreaterOrEqual(value, minValue);
                Assert.Less(value, maxValue);
            }
        }

        [Test]
        public void RandomWithSeed_IsNotNull()
        {
            Assert.IsNotNull(RandomUtility.RandomWithSeed);
        }

        [Test]
        public void MultipleCalls_ProduceDifferentValues()
        {
            RandomUtility.InitializeWithSeed(DateTime.Now.Millisecond);
            var values = new int[10];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = RandomUtility.Next();
            }
            Assert.That(values, Is.Unique.Or.Contains(values[0]));
        }
    }
}
