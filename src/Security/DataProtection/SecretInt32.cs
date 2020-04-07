// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace ReSharp.Security.DataProtection
{
    /// <summary>
    /// Represents <see cref="int" /> value with data protection in memory.
    /// Implements the <see cref="System.IEquatable{T}" />
    /// Implements the <see cref="System.IComparable{T}" />
    /// Implements the <see cref="System.Runtime.Serialization.ISerializable" />
    /// </summary>
    /// <seealso cref="System.IEquatable{T}" />
    /// <seealso cref="System.IComparable{T}" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    [Serializable]
    public struct SecretInt32 : IEquatable<SecretInt32>, IComparable<SecretInt32>, ISerializable
    {
        #region Fields

        private int check;
        private int value;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretInt32" /> struct with the <see cref="int" /> value to encrypt.
        /// </summary>
        /// <param name="value">The <see cref="int" /> value to encrypt.</param>
        public SecretInt32(int value)
        {
            this.value = DataProtectionProvider.Protect(value, out check);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretInt32" /> struct.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public SecretInt32(SerializationInfo info, StreamingContext context)
        {
            int value = info.GetInt32("v");
            this.value = DataProtectionProvider.Protect(value, out check);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the original <see cref="int" /> value.
        /// </summary>
        /// <value>The original <see cref="int" /> value.</value>
        public int Value
        {
            get => DataProtectionProvider.Unprotect(value, check);
            set => this.value = DataProtectionProvider.Protect(value, out check);
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt32" /> to <see cref="double" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt32" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(SecretInt32 value)
        {
            double result = value.Value;
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt32" /> to <see cref="float" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt32" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator float(SecretInt32 value)
        {
            float result = value.Value;
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt32" /> to <see cref="int" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt32" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(SecretInt32 value)
        {
            return value.Value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt32" /> to <see cref="long" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt32" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator long(SecretInt32 value)
        {
            long result = value.Value;
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="int" /> to <see cref="SecretInt32" />.
        /// </summary>
        /// <param name="value">The <see cref="int" /> value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecretInt32(int value)
        {
            return new SecretInt32(value);
        }

        /// <summary>
        /// Subtracts a <see cref="SecretInt32" /> structure from a <see cref="SecretInt32" /> structure.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt32" /> structure.</param>
        /// <returns>A <see cref="SecretInt32" /> structure whose <see name="Value" /> property contains the results of the subtraction.</returns>
        public static SecretInt32 operator -(SecretInt32 x, SecretInt32 y)
        {
            int result = x.Value - y.Value;
            return new SecretInt32(result);
        }

        /// <summary>
        /// Decrements the <see cref="SecretInt32" /> operand by one.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// A <see cref="SecretInt32" /> structure whose <see name="Value" /> property contains the value of <c>value</c> decremented by 1.
        /// </returns>
        public static SecretInt32 operator --(SecretInt32 value)
        {
            int result = value.Value;
            return new SecretInt32(--result);
        }

        /// <summary>
        /// Performs a logical comparison of the two <see cref="SecretInt32" /> parameters to determine whether they are not equal.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt32" /> structure.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="SecretInt32" /> structures are not equal or <c>false</c> if the two <see cref="SecretInt32" />
        /// structures are equal.
        /// </returns>
        public static bool operator !=(SecretInt32 left, SecretInt32 right) => !left.Equals(right);

        /// <summary>
        /// Computes the remainder after dividing the first <see cref="SecretInt32" /> parameter by the second.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt32" /> structure.</param>
        /// <returns>A <see cref="SecretInt32" /> structure whose <see name="Value" /> property contains the remainder.</returns>
        public static SecretInt32 operator %(SecretInt32 x, SecretInt32 y)
        {
            int result = x.Value % y.Value;
            return new SecretInt32(result);
        }

        /// <summary>
        /// Computes the product of the two <see cref="SecretInt32" /> parameters.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt32" /> structure.</param>
        /// <returns>A <see cref="SecretInt32" /> structure whose <see name="Value" /> property contains the product of the two parameters.</returns>
        public static SecretInt32 operator *(SecretInt32 x, SecretInt32 y)
        {
            int result = x.Value * y.Value;
            return new SecretInt32(result);
        }

        /// <summary>
        /// Divides the first <see cref="SecretInt32" /> parameter from the second.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt32" /> structure.</param>
        /// <returns>A <see cref="SecretInt32" /> structure whose <see name="Value" /> property contains the result of the division.</returns>
        public static SecretInt32 operator /(SecretInt32 x, SecretInt32 y)
        {
            int result = x.Value / y.Value;
            return new SecretInt32(result);
        }

        /// <summary>
        /// Computes the sum of the two specified <see cref="SecretInt32" /> structures.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt32" /> structure.</param>
        /// <returns>
        /// A <see cref="SecretInt32" /> structure whose <see name="Value" /> property contains the sum of the specified <see cref="SecretInt32" /> structures.
        /// </returns>
        public static SecretInt32 operator +(SecretInt32 x, SecretInt32 y)
        {
            int result = x.Value + y.Value;
            return new SecretInt32(result);
        }

        /// <summary>
        /// Increments the <see cref="SecretInt32" /> operand by 1.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// A <see cref="SecretInt32" /> structure whose <see name="Value" /> property contains the value of <c>value</c> incremented by 1.
        /// </returns>
        public static SecretInt32 operator ++(SecretInt32 value)
        {
            int result = value.Value;
            return new SecretInt32(++result);
        }

        /// <summary>
        /// Compares the two <see cref="SecretInt32" /> parameters to determine whether the first is less than the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt32" /> structure.</param>
        /// <returns><c>true</c> if the first instance is less than the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator <(SecretInt32 left, SecretInt32 right)
        {
            return left.Value < right.Value;
        }

        /// <summary>
        /// Compares the two <see cref="SecretInt32" /> parameters to determine whether the first is less than or equal to the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt32" /> structure.</param>
        /// <returns><c>true</c> if the first instance is less than or equal to the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator <=(SecretInt32 left, SecretInt32 right)
        {
            return left.Value <= right.Value;
        }

        /// <summary>
        /// Performs a logical comparison of the two <see cref="SecretInt32" /> parameters to determine whether they are equal.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt32" /> structure.</param>
        /// <returns><c>true</c> if the two instances are equal or <c>false</c> if the two instances are not equal.</returns>
        public static bool operator ==(SecretInt32 left, SecretInt32 right) => left.Equals(right);

        /// <summary>
        /// Compares the two <see cref="SecretInt32" /> parameters to determine whether the first is greater than the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt32" /> structure.</param>
        /// <returns><c>true</c> if the first instance is greater than the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator >(SecretInt32 left, SecretInt32 right)
        {
            return left.Value > right.Value;
        }

        /// <summary>
        /// Compares the two <see cref="SecretInt32" /> parameters to determine whether the first is greater than or equal to the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt32" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt32" /> structure.</param>
        /// <returns><c>true</c> if the first instance is greater than or equal to the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator >=(SecretInt32 left, SecretInt32 right)
        {
            return left.Value >= right.Value;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance
        /// precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(SecretInt32 other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            var result = (SecretInt32)obj;
            return result == this;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(SecretInt32 other) => Value.Equals(other.Value);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Populates a <see cref="System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("v", Value);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion Methods
    }
}