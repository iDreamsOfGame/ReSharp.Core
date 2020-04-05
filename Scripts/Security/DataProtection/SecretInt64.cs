// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace ReSharp.Security.DataProtection
{
    /// <summary>
    /// Represents <see cref="long" /> value with data protection in memory.
    /// Implements the <see cref="System.IEquatable{T}" />
    /// Implements the <see cref="System.IComparable{T}" />
    /// Implements the <see cref="System.Runtime.Serialization.ISerializable" />
    /// </summary>
    /// <seealso cref="System.IEquatable{T}" />
    /// <seealso cref="System.IComparable{T}" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    [Serializable]
    public struct SecretInt64 : IEquatable<SecretInt64>, IComparable<SecretInt64>, ISerializable
    {
        #region Fields

        private long check;
        private long value;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretInt64" /> struct with the <see cref="long" /> value to encrypt.
        /// </summary>
        /// <param name="value">The <see cref="long" /> value to encrypt.</param>
        public SecretInt64(long value)
        {
            this.value = DataProtectionProvider.Protect(value, out check);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretInt64" /> struct.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public SecretInt64(SerializationInfo info, StreamingContext context)
        {
            long value = info.GetInt64("v");
            this.value = DataProtectionProvider.Protect(value, out check);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the original <see cref="long" /> value.
        /// </summary>
        /// <value>The original <see cref="long" /> value.</value>
        public long Value
        {
            get => DataProtectionProvider.Unprotect(value, check);
            set => this.value = DataProtectionProvider.Protect(value, out check);
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt64" /> to <see cref="double" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt64" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(SecretInt64 value)
        {
            double result = value.Value;
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt64" /> to <see cref="float" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt64" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator float(SecretInt64 value)
        {
            float result = value.Value;
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt64" /> to <see cref="long" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt64" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator long(SecretInt64 value)
        {
            return value.Value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="long" /> to <see cref="SecretInt64" />.
        /// </summary>
        /// <param name="value">The <see cref="long" /> value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecretInt64(long value)
        {
            return new SecretInt64(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt32" /> to <see cref="SecretInt64" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt32" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecretInt64(SecretInt32 value)
        {
            return new SecretInt64(value.Value);
        }

        /// <summary>
        /// Subtracts a <see cref="SecretInt64" /> structure from a <see cref="SecretInt64" /> structure.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt64" /> structure.</param>
        /// <returns>A <see cref="SecretInt64" /> structure whose <see name="Value" /> property contains the results of the subtraction.</returns>
        public static SecretInt64 operator -(SecretInt64 x, SecretInt64 y)
        {
            long result = x.Value - y.Value;
            return new SecretInt64(result);
        }

        /// <summary>
        /// Decrements the <see cref="SecretInt64" /> operand by one.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// A <see cref="SecretInt64" /> structure whose <see name="Value" /> property contains the value of <c>value</c> decremented by 1.
        /// </returns>
        public static SecretInt64 operator --(SecretInt64 value)
        {
            long result = value.Value;
            return new SecretInt64(--result);
        }

        /// <summary>
        /// Performs a logical comparison of the two <see cref="SecretInt64" /> parameters to determine whether they are not equal.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt64" /> structure.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="SecretInt64" /> structures are not equal or <c>false</c> if the two <see cref="SecretInt64" />
        /// structures are equal.
        /// </returns>
        public static bool operator !=(SecretInt64 left, SecretInt64 right) => !left.Equals(right);

        /// <summary>
        /// Computes the remainder after dividing the first <see cref="SecretInt64" /> parameter by the second.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt64" /> structure.</param>
        /// <returns>A <see cref="SecretInt64" /> structure whose <see name="Value" /> property contains the remainder.</returns>
        public static SecretInt64 operator %(SecretInt64 x, SecretInt64 y)
        {
            long result = x.Value % y.Value;
            return new SecretInt64(result);
        }

        /// <summary>
        /// Computes the product of the two <see cref="SecretInt64" /> parameters.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt64" /> structure.</param>
        /// <returns>A <see cref="SecretInt64" /> structure whose <see name="Value" /> property contains the product of the two parameters.</returns>
        public static SecretInt64 operator *(SecretInt64 x, SecretInt64 y)
        {
            long result = x.Value * y.Value;
            return new SecretInt64(result);
        }

        /// <summary>
        /// Divides the first <see cref="SecretInt64" /> parameter from the second.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt64" /> structure.</param>
        /// <returns>A <see cref="SecretInt64" /> structure whose <see name="Value" /> property contains the result of the division.</returns>
        public static SecretInt64 operator /(SecretInt64 x, SecretInt64 y)
        {
            long result = x.Value / y.Value;
            return new SecretInt64(result);
        }

        /// <summary>
        /// Computes the sum of the two specified <see cref="SecretInt64" /> structures.
        /// </summary>
        /// <param name="x">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="y">A <see cref="SecretInt64" /> structure.</param>
        /// <returns>
        /// A <see cref="SecretInt64" /> structure whose <see name="Value" /> property contains the sum of the specified <see cref="SecretInt64" /> structures.
        /// </returns>
        public static SecretInt64 operator +(SecretInt64 x, SecretInt64 y)
        {
            long result = x.Value + y.Value;
            return new SecretInt64(result);
        }

        /// <summary>
        /// Increments the <see cref="SecretInt64" /> operand by 1.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// A <see cref="SecretInt64" /> structure whose <see name="Value" /> property contains the value of <c>value</c> incremented by 1.
        /// </returns>
        public static SecretInt64 operator ++(SecretInt64 value)
        {
            long result = value.Value;
            return new SecretInt64(++result);
        }

        /// <summary>
        /// Compares the two <see cref="SecretInt64" /> parameters to determine whether the first is less than the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt64" /> structure.</param>
        /// <returns><c>true</c> if the first instance is less than the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator <(SecretInt64 left, SecretInt64 right)
        {
            return left.Value < right.Value;
        }

        /// <summary>
        /// Compares the two <see cref="SecretInt64" /> parameters to determine whether the first is less than or equal to the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt64" /> structure.</param>
        /// <returns><c>true</c> if the first instance is less than or equal to the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator <=(SecretInt64 left, SecretInt64 right)
        {
            return left.Value <= right.Value;
        }

        /// <summary>
        /// Performs a logical comparison of the two <see cref="SecretInt64" /> parameters to determine whether they are equal.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt64" /> structure.</param>
        /// <returns><c>true</c> if the two instances are equal or <c>false</c> if the two instances are not equal.</returns>
        public static bool operator ==(SecretInt64 left, SecretInt64 right) => left.Equals(right);

        /// <summary>
        /// Compares the two <see cref="SecretInt64" /> parameters to determine whether the first is greater than the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt64" /> structure.</param>
        /// <returns><c>true</c> if the first instance is greater than the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator >(SecretInt64 left, SecretInt64 right)
        {
            return left.Value > right.Value;
        }

        /// <summary>
        /// Compares the two <see cref="SecretInt64" /> parameters to determine whether the first is greater than or equal to the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretInt64" /> structure.</param>
        /// <param name="right">A <see cref="SecretInt64" /> structure.</param>
        /// <returns><c>true</c> if the first instance is greater than or equal to the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator >=(SecretInt64 left, SecretInt64 right)
        {
            return left.Value >= right.Value;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance
        /// precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(SecretInt64 other)
        {
            return value.CompareTo(other.Value);
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

            var result = (SecretInt64)obj;
            return result == this;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(SecretInt64 other) => value.Equals(other.value);

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