// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace ReSharp.Security.DataProtection
{
    /// <summary>
    /// Represents <see cref="double" /> value with data protection in memory.
    /// Implements the <see cref="System.IEquatable{T}" />
    /// Implements the <see cref="System.IComparable{T}" />
    /// Implements the <see cref="System.Runtime.Serialization.ISerializable" />
    /// </summary>
    /// <seealso cref="System.IEquatable{T}" />
    /// <seealso cref="System.IComparable{T}" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    [Serializable]
    public struct SecretDouble : IEquatable<SecretDouble>, IComparable<SecretDouble>, ISerializable
    {
        private long check;

        private long value;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretDouble" /> struct with the <see cref="double" /> to encrypt.
        /// </summary>
        /// <param name="value">The <see cref="double" /> to encrypt.</param>
        public SecretDouble(double value)
        {
            this.value = DataProtectionProvider.Protect(value, out check);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretDouble" /> struct.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public SecretDouble(SerializationInfo info, StreamingContext context)
        {
            value = DataProtectionProvider.Protect(info.GetDouble("v"), out check);
        }

        /// <summary>
        /// Gets the original <see cref="double" /> value.
        /// </summary>
        /// <value>The original <see cref="double" /> value.</value>
        public double Value
        {
            get => DataProtectionProvider.UnprotectDouble(value, check);
            set => this.value = DataProtectionProvider.Protect(value, out check);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretDouble" /> to <see cref="double" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretDouble" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(SecretDouble value) => value.Value;

        /// <summary>
        /// Performs an implicit conversion from <see cref="double" /> to <see cref="SecretDouble" />.
        /// </summary>
        /// <param name="value">The <see cref="double" /> value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecretDouble(double value) => new SecretDouble(value);

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt32" /> to <see cref="SecretDouble" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt32" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecretDouble(SecretInt32 value) => new SecretDouble(value.Value);

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretInt64" /> to <see cref="SecretDouble" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretInt64" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecretDouble(SecretInt64 value) => new SecretDouble(value.Value);

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecretSingle" /> to <see cref="SecretDouble" />.
        /// </summary>
        /// <param name="value">The <see cref="SecretSingle" /> structure.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecretDouble(SecretSingle value) => new SecretDouble(value.Value);

        /// <summary>
        /// Subtracts a <see cref="SecretDouble" /> structure from a <see cref="SecretDouble" /> structure.
        /// </summary>
        /// <param name="x">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="y">A <see cref="SecretDouble" /> structure.</param>
        /// <returns>A <see cref="SecretDouble" /> structure whose <see name="Value" /> property contains the results of the subtraction.</returns>
        public static SecretDouble operator -(SecretDouble x, SecretDouble y) => new SecretDouble(x.Value - y.Value);

        /// <summary>
        /// Decrements the <see cref="SecretDouble" /> operand by one.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// A <see cref="SecretDouble" /> structure whose <see name="Value" /> property contains the value of <c>value</c> decremented by 1.
        /// </returns>
        public static SecretDouble operator --(SecretDouble value)
        {
            var result = value.Value;
            return new SecretDouble(--result);
        }

        /// <summary>
        /// Performs a logical comparison of the two <see cref="SecretDouble" /> parameters to determine whether they are not equal.
        /// </summary>
        /// <param name="left">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="right">A <see cref="SecretDouble" /> structure.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="SecretDouble" /> structures are not equal or <c>false</c> if the two <see cref="SecretDouble" />
        /// structures are equal.
        /// </returns>
        public static bool operator !=(SecretDouble left, SecretDouble right) => !left.Equals(right);

        /// <summary>
        /// Computes the remainder after dividing the first <see cref="SecretDouble" /> parameter by the second.
        /// </summary>
        /// <param name="x">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="y">A <see cref="SecretDouble" /> structure.</param>
        /// <returns>A <see cref="SecretDouble" /> structure whose <see name="Value" /> property contains the remainder.</returns>
        public static SecretDouble operator %(SecretDouble x, SecretDouble y) => new SecretDouble(x.Value % y.Value);

        /// <summary>
        /// Computes the product of the two <see cref="SecretDouble" /> parameters.
        /// </summary>
        /// <param name="x">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="y">A <see cref="SecretDouble" /> structure.</param>
        /// <returns>A <see cref="SecretDouble" /> structure whose <see name="Value" /> property contains the product of the two parameters.</returns>
        public static SecretDouble operator *(SecretDouble x, SecretDouble y) => new SecretDouble(x.Value * y.Value);

        /// <summary>
        /// Divides the first <see cref="SecretDouble" /> parameter from the second.
        /// </summary>
        /// <param name="x">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="y">A <see cref="SecretDouble" /> structure.</param>
        /// <returns>A <see cref="SecretDouble" /> structure whose <see name="Value" /> property contains the result of the division.</returns>
        public static SecretDouble operator /(SecretDouble x, SecretDouble y) => new SecretDouble(x.Value / y.Value);

        /// <summary>
        /// Computes the sum of the two specified <see cref="SecretDouble" /> structures.
        /// </summary>
        /// <param name="x">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="y">A <see cref="SecretDouble" /> structure.</param>
        /// <returns>
        /// A <see cref="SecretDouble" /> structure whose <see name="Value" /> property contains the sum of the specified <see cref="SecretDouble" /> structures.
        /// </returns>
        public static SecretDouble operator +(SecretDouble x, SecretDouble y) => new SecretDouble(x.Value + y.Value);

        /// <summary>
        /// Increments the <see cref="SecretDouble" /> operand by 1.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// A <see cref="SecretDouble" /> structure whose <see name="Value" /> property contains the value of <c>value</c> incremented by 1.
        /// </returns>
        public static SecretDouble operator ++(SecretDouble value)
        {
            var result = value.Value;
            return new SecretDouble(++result);
        }

        /// <summary>
        /// Compares the two <see cref="SecretDouble" /> parameters to determine whether the first is less than the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="right">A <see cref="SecretDouble" /> structure.</param>
        /// <returns><c>true</c> if the first instance is less than the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator <(SecretDouble left, SecretDouble right) => left.Value < right.Value;

        /// <summary>
        /// Compares the two <see cref="SecretDouble" /> parameters to determine whether the first is less than or equal to the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="right">A <see cref="SecretDouble" /> structure.</param>
        /// <returns><c>true</c> if the first instance is less than or equal to the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator <=(SecretDouble left, SecretDouble right) => left.Value <= right.Value;

        /// <summary>
        /// Performs a logical comparison of the two <see cref="SecretDouble" /> parameters to determine whether they are equal.
        /// </summary>
        /// <param name="left">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="right">A <see cref="SecretDouble" /> structure.</param>
        /// <returns><c>true</c> if the two instances are equal or <c>false</c> if the two instances are not equal.</returns>
        public static bool operator ==(SecretDouble left, SecretDouble right) => left.Equals(right);

        /// <summary>
        /// Compares the two <see cref="SecretDouble" /> parameters to determine whether the first is greater than the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="right">A <see cref="SecretDouble" /> structure.</param>
        /// <returns><c>true</c> if the first instance is greater than the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator >(SecretDouble left, SecretDouble right) => left.Value > right.Value;

        /// <summary>
        /// Compares the two <see cref="SecretDouble" /> parameters to determine whether the first is greater than or equal to the second.
        /// </summary>
        /// <param name="left">A <see cref="SecretDouble" /> structure.</param>
        /// <param name="right">A <see cref="SecretDouble" /> structure.</param>
        /// <returns><c>true</c> if the first instance is greater than or equal to the second instance. Otherwise, <c>false</c>.</returns>
        public static bool operator >=(SecretDouble left, SecretDouble right) => left.Value >= right.Value;

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance
        /// precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(SecretDouble other) => value.CompareTo(other.Value);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            var result = (SecretDouble)obj;
            return result == this;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(SecretDouble other) => value.Equals(other.value);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => value.GetHashCode();

        /// <summary>
        /// Populates a <see cref="System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue("v", Value);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}