using NUnit.Framework;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace ReSharp.Extensions.Tests
{
    [TestFixture]
    public class StringReferenceEqualityComparerTests
    {
        private StringReferenceEqualityComparer comparer;

        [SetUp]
        public void SetUp()
        {
            // 使用单例实例
            comparer = StringReferenceEqualityComparer.Instance;
        }

        [Test]
        public void Instance_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = StringReferenceEqualityComparer.Instance;
            var instance2 = StringReferenceEqualityComparer.Instance;

            // Assert
            Assert.AreSame(instance1, instance2, "Instance should return the same singleton object.");
        }

        [Test]
        public void Equals_WithSameReference_ShouldReturnTrue()
        {
            // Arrange
            string str = "test";
            string sameRef = str;

            // Act
            bool result = comparer.Equals(str, sameRef);

            // Assert
            Assert.IsTrue(result, "Strings with the same reference should be equal.");
        }

        [Test]
        public void Equals_WithInternedStrings_ShouldReturnTrue()
        {
            // Arrange
            // String literals are usually interned by the CLR
            string str1 = "hello world";
            string str2 = "hello world";

            // Act
            bool result = comparer.Equals(str1, str2);

            // Assert
            Assert.IsTrue(result, "Interned strings with the same content should have the same reference and be equal.");
        }

        [Test]
        public void Equals_WithDifferentReferencesButSameContent_ShouldReturnFalse()
        {
            // Arrange
            // Create a new string object explicitly to ensure it's not the same reference as the literal
            char[] chars = { 't', 'e', 's', 't' };
            string str1 = new string(chars);
            string str2 = "test"; // This might be interned, but str1 is definitely a new object on heap

            // Ensure they are actually different references for the test validity
            Assert.AreNotSame(str1, str2, "Precondition failed: Strings should not have the same reference.");

            // Act
            bool result = comparer.Equals(str1, str2);

            // Assert
            Assert.IsFalse(result, "Strings with different references should not be equal, even if content is the same.");
        }

        [Test]
        public void Equals_WithNullAndNull_ShouldReturnTrue()
        {
            // Arrange
            string str1 = null;
            string str2 = null;

            // Act
            bool result = comparer.Equals(str1, str2);

            // Assert
            Assert.IsTrue(result, "Two null strings should be considered equal by reference.");
        }

        [Test]
        public void Equals_WithNullAndNonNull_ShouldReturnFalse()
        {
            // Arrange
            string str1 = null;
            string str2 = "test";

            // Act
            bool result = comparer.Equals(str1, str2);

            // Assert
            Assert.IsFalse(result, "Null and non-null strings should not be equal.");
        }

        [Test]
        public void GetHashCode_WithSameReference_ShouldReturnSameHashCode()
        {
            // Arrange
            string str = "hash test";
            
            // Act
            int hash1 = comparer.GetHashCode(str);
            int hash2 = comparer.GetHashCode(str);

            // Assert
            Assert.AreEqual(hash1, hash2, "GetHashCode should return the same value for the same reference.");
        }

        [Test]
        public void GetHashCode_WithDifferentReferences_ShouldReflectReferenceIdentity()
        {
            // Arrange
            string str1 = new string(new[] { 'a', 'b' });
            string str2 = new string(new[] { 'a', 'b' });

            // Precondition check
            Assert.AreNotSame(str1, str2, "Precondition failed: Objects must have different references.");

            // Act
            int hash1 = comparer.GetHashCode(str1);
            int hash2 = comparer.GetHashCode(str2);

            // Assert
            // Note: While hash collisions are theoretically possible, RuntimeHelpers.GetHashCode 
            // is designed to be based on object identity. For distinct objects in a short-lived test,
            // they are overwhelmingly likely to be different.
            Assert.AreNotEqual(hash1, hash2, "Different references should typically produce different hash codes using RuntimeHelpers.GetHashCode.");
        }
        
        [Test]
        public void Equals_InternedVsNewString_ShouldBeFalse()
        {
            // Arrange
            string interned = string.Intern("unique_test_string_123");
            string newStr = new string("unique_test_string_123".ToCharArray());
            
            Assert.AreNotSame(interned, newStr);

            // Act
            bool result = comparer.Equals(interned, newStr);

            // Assert
            Assert.IsFalse(result);
        }
    }
}