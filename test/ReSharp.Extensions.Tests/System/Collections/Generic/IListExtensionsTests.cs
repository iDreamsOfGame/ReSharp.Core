using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class IListExtensionsTests
    {
        [Test]
        public void FisherYatesShuffleTest()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            list.FisherYatesShuffle(Guid.NewGuid().GetHashCode());
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Assert.IsTrue(list[0] != list[1] && list[1] != list[2] && list[2] != list[3] && list[3] != list[4]);
        }
    }
}