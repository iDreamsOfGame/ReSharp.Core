using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Tests
{
    public class IdInfo
    {
        public int IdNumber;
    }

    public class Person
    {
        public int Age;
        public string Name;
        public IdInfo IdInfo;
    }

    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void ShallowCloneTest()
        {
            Person p1 = new Person
            {
                Age = 42,
                Name = "Sam",
                IdInfo = new IdInfo() { IdNumber = 6565 }
            };

            Person p2 = p1.ShallowClone();
            p2.IdInfo.IdNumber = 65;

            Assert.AreEqual(p1.IdInfo.IdNumber, p2.IdInfo.IdNumber);
        }

        [TestMethod]
        public void DeepCloneTest()
        {
            Person p1 = new Person
            {
                Age = 42,
                Name = "Sam",
                IdInfo = new IdInfo() { IdNumber = 6565 }
            };

            Person p2 = p1.DeepClone() as Person;
            p2.IdInfo.IdNumber = 65;
            Assert.AreNotEqual(p1.IdInfo.IdNumber, p2.IdInfo.IdNumber);
        }

        [TestMethod]
        public void GenericDeepCloneTest()
        {
            Person p1 = new Person
            {
                Age = 42,
                Name = "Sam",
                IdInfo = new IdInfo() { IdNumber = 6565 }
            };

            Person p2 = p1.DeepClone<Person>();
            p2.IdInfo.IdNumber = 65;
            Assert.AreNotEqual(p1.IdInfo.IdNumber, p2.IdInfo.IdNumber);
        }
    }
}
