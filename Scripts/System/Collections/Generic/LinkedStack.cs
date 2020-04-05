// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a linked stack. Implements the <see cref="System.Collections.Generic.IEnumerable{T}" /> Implements the <see
    /// cref="System.Collections.ICollection" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    /// <seealso cref="System.Collections.ICollection" />
    [DebuggerDisplay("Count = {Count}")]
    [ComVisible(false)]
    public class LinkedStack<T> : IEnumerable<T>, ICollection
    {
        #region Fields

        private readonly LinkedList<T> list;

        [NonSerialized]
        private object syncRoot;

        private int version;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedStack{T}" /> class.
        /// </summary>
        public LinkedStack()
        {
            list = new LinkedList<T>();
            version = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedStack{T}" /> class that contains elements copied from the specified collection and has
        /// sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public LinkedStack(IEnumerable<T> collection)
        {
            list = new LinkedList<T>(collection);
            version = 0;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="LinkedStack{T}" />.
        /// </summary>
        /// <value>The number of elements contained in the <see cref="LinkedStack{T}" />.</value>
        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                if (syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(ref syncRoot, new object(), null);
                }

                return syncRoot;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Removes all objects from the <see cref="LinkedStack{T}" />.
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }

        /// <summary>
        /// Determines whether an element is in the <see cref="LinkedStack{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="LinkedStack{T}" />. The value can be null for reference types.</param>
        /// <returns><c>true</c> if <c>item</c> is found in the <see cref="LinkedStack{T}" />; otherwise, <c>false</c>.</returns>
        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// Copies the <see cref="LinkedStack{T}" /> to an existing one-dimensional <see cref="System.Array" />, starting at the specified array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array" /> that is the destination of the elements copied from <see cref="LinkedStack{T}" />. The
        /// <see cref="System.Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <c>array</c> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><c>array</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>arrayIndex</c> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// The number of elements in the source <see cref="LinkedStack{T}" /> is greater than the available space from <c>arrayIndex</c> to the end
        /// of the destination <c>array</c>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "arrayIndex is less than zero. ");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("The number of elements in the source LinkedStack<T> is greater than the available space from index to the end of the destination array.");
            }

            list.CopyTo(array, arrayIndex);
            Array.Reverse(array, arrayIndex, Count);
        }

        /// <summary>
        /// Returns an enumerator for the <see cref="LinkedStack{T}" />.
        /// </summary>
        /// <returns>An <see cref="LinkedStack{T}.Enumerator" /> for the <see cref="LinkedStack{T}" />.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Returns the object at the top of the <see cref="LinkedStack{T}" /> without removing it.
        /// </summary>
        /// <returns>The object at the top of the <see cref="LinkedStack{T}" />.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="LinkedStack{T}" /> is empty.</exception>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Empty stack");
            }

            return list.Last.Value;
        }

        /// <summary>
        /// Removes and returns the object at the top of the <see cref="LinkedStack{T}" />.
        /// </summary>
        /// <returns>The object removed from the top of the <see cref="LinkedStack{T}" />.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="LinkedStack{T}" /> is empty.</exception>
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Empty stack");
            }

            version++;
            T lastItem = list.Last.Value;
            list.RemoveLast();
            return lastItem;
        }

        /// <summary>
        /// Inserts an object at the top of the <see cref="LinkedStack{T}" />.
        /// </summary>
        /// <param name="item">The object to push onto the <see cref="LinkedStack{T}" />. The value can be <c>null</c> for reference types.</param>
        public void Push(T item)
        {
            list.AddLast(item);
            version++;
        }

        /// <summary>
        /// Copies the <see cref="LinkedStack{T}" /> to a new array.
        /// </summary>
        /// <returns>A new array containing copies of the elements of the <see cref="LinkedStack{T}" />.</returns>
        public T[] ToArray()
        {
            T[] array = new T[list.Count];
            LinkedListNode<T> node = list.Last;
            int index = 0;

            while (node != null)
            {
                array[index] = node.Value;

                if (node.Previous != list.Last)
                {
                    node = node.Previous;
                }
                else
                {
                    node = null;
                }
            }

            return array;
        }

        /// <summary>
        /// Tries to get the object at the top of the <see cref="LinkedStack{T}" /> without removing it, and returns a value that indicates whether
        /// the object exists.
        /// </summary>
        /// <param name="result">
        /// When this method returns, contains the object at the top of the <see cref="LinkedStack{T}" />, or default value of <c>T</c>.
        /// </param>
        /// <returns><c>true</c> if the object at the top of the <see cref="LinkedStack{T}" /> exists, <c>false</c> otherwise.</returns>
        public bool TryPeek(out T result)
        {
            result = default;

            if (list.Last != null)
            {
                result = list.Last.Value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to remove and get the object at the top of the <see cref="LinkedStack{T}" />, and returns a value that indicates whether the object exists.
        /// </summary>
        /// <param name="result">
        /// When this method returns, contains the object at the top of the <see cref="LinkedStack{T}" />, or default value of <c>T</c>.
        /// </param>
        /// <returns><c>true</c> if the object at the top of the <see cref="LinkedStack{T}" /> exists, <c>false</c> otherwise.</returns>
        public bool TryPop(out T result)
        {
            result = default;

            if (list.Last != null)
            {
                result = list.Last.Value;
                list.RemoveLast();
                version++;
                return true;
            }

            return false;
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "arrayIndex is less than zero. ");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("The number of elements in the source LinkedStack<T> is greater than the available space from index to the end of the destination array.");
            }

            ICollection collection = list as ICollection;
            collection.CopyTo(array, arrayIndex);
            Array.Reverse(array, arrayIndex, Count);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion Methods

        #region Structs

        /// <summary>
        /// Enumerates the elements of a <see cref="LinkedStack{T}" />. Implements the <see cref="System.Collections.Generic.IEnumerator{T}" />.
        /// Implements the <see cref="System.Collections.IEnumerator" />.
        /// </summary>
        /// <seealso cref="System.Collections.Generic.IEnumerator{T}" />
        /// <seealso cref="System.Collections.IEnumerator" />
        [Serializable()]
        [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "not an expected scenario")]
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            #region Fields

            private T currentElement;
            private int index;
            private LinkedListNode<T> node;
            private LinkedStack<T> stack;
            private int version;

            #endregion Fields

            #region Constructors

            internal Enumerator(LinkedStack<T> stack)
            {
                this.stack = stack;
                version = stack.version;
                index = -2;
                node = null;
                currentElement = default(T);
            }

            #endregion Constructors

            #region Properties

            /// <summary>
            /// Gets the element at the current position of the enumerator. Implements the <see
            /// cref="System.Collections.Generic.IEnumerator{T}.Current" />.
            /// </summary>
            /// <value>The element in the <see cref="LinkedStack{T}" /> at the current position of the enumerator.</value>
            /// <exception cref="InvalidOperationException">
            /// The enumerator is positioned before the first element of the collection or after the last element.
            /// </exception>
            public T Current
            {
                get
                {
                    if (index == -2)
                    {
                        throw new InvalidOperationException("Enum not started");
                    }

                    if (index == -1)
                    {
                        throw new InvalidOperationException("Enum ended");
                    }

                    return currentElement;
                }
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator. Implements the <see cref="System.Collections.IEnumerator.Current" />.
            /// </summary>
            /// <value>The element in the collection at the current position of the enumerator.</value>
            /// <exception cref="InvalidOperationException">
            /// The enumerator is positioned before the first element of the collection or after the last element.
            /// </exception>
            object IEnumerator.Current
            {
                get
                {
                    if (index == -2)
                    {
                        throw new InvalidOperationException("Enum not started");
                    }

                    if (index == -1)
                    {
                        throw new InvalidOperationException("Enum ended");
                    }

                    return currentElement;
                }
            }

            #endregion Properties

            #region Methods

            /// <summary>
            /// Releases all resources used by the <see cref="LinkedStack{T}.Enumerator" />.
            /// </summary>
            public void Dispose()
            {
                index = -1;
            }

            /// <summary>
            /// Advances the enumerator to the next element of the <see cref="LinkedStack{T}" />. Implements the <see
            /// cref="System.Collections.IEnumerator.MoveNext" />.
            /// </summary>
            /// <returns>
            /// <c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            public bool MoveNext()
            {
                bool retval;

                if (version != stack.version)
                {
                    throw new InvalidOperationException("Enum failed version");
                }

                if (index == -2)
                {
                    node = stack.list.Last;
                    index = stack.Count - 1;
                    retval = (index >= 0);

                    if (retval)
                    {
                        currentElement = node.Value;
                    }

                    return retval;
                }

                if (index == -1)
                {
                    return false;
                }

                retval = (--index >= 0);

                if (retval)
                {
                    currentElement = node.Value;
                    node = node.Previous;
                }
                else
                {
                    currentElement = default(T);
                }

                return retval;
            }

            void IEnumerator.Reset()
            {
                if (version != stack.version)
                {
                    throw new InvalidOperationException("Enum failed version");
                }

                index = -2;
                currentElement = default(T);
            }

            #endregion Methods
        }

        #endregion Structs
    }
}