// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a tree data structure.
    /// Implements the <see cref="System.Collections.Generic.IEnumerable{T}" />
    /// Implements the <see cref="System.Collections.IEnumerable" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    /// <seealso cref="System.Collections.IEnumerable" />
    [ComVisible(false)]
    public class Tree<T> : IEnumerable<T>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Tree{T}" /> class.
        /// </summary>
        public Tree()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tree{T}" /> class that have a root <see cref="TreeNode{T}" />.
        /// </summary>
        /// <param name="root">The root <see cref="TreeNode{T}" />.</param>
        public Tree(TreeNode<T> root)
            : this()
        {
            Root = root;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the root <see cref="TreeNode{T}" />.
        /// </summary>
        /// <value>The root <see cref="TreeNode{T}" />.</value>
        public TreeNode<T> Root
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
        public IEnumerator GetEnumerator()
        {
            yield return (this as IEnumerable<T>).GetEnumerator();
        }

        /// <summary>
        /// Performs post-order traversal.
        /// </summary>
        /// <returns>The <see cref="System.Collections.Generic.IList{T}" /> contains the results of post-order traversal.</returns>
        public IList<T> PostorderTraverse()
        {
            if (Root == null)
                return null;
            
            IList<T> list = new List<T>();
            PostorderTraverse(Root, list);
            return list;
        }

        /// <summary>
        /// Performs pre-order traversal.
        /// </summary>
        /// <returns>The <see cref="System.Collections.Generic.IList{T}" /> contains the results of pre-order traversal.</returns>
        public IList<T> PreoderTraverse()
        {
            if (Root == null) 
                return null;
            
            IList<T> list = new List<T>();
            PreoderTraverse(Root, list);
            return list;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            IList<T> list = PreoderTraverse();

            foreach (T item in list)
            {
                yield return item;
            }
        }

        private void PostorderTraverse(TreeNode<T> node, IList<T> list)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            foreach (var child in node.children)
            {
                PostorderTraverse(child, list);
            }

            list.Add(node.Value);
        }

        private void PreoderTraverse(TreeNode<T> node, IList<T> list)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            list.Add(node.Value);

            foreach (var child in node.children)
            {
                PreoderTraverse(child, list);
            }
        }

        #endregion Methods
    }

    /// <summary>
    /// Represents a node in a <see cref="Tree{T}" />. This class cannot be inherited. Implements the <see
    /// cref="System.Collections.Generic.ICollection{T}" /> Implements the <see cref="System.Collections.ICollection" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.ICollection{T}" />
    /// <seealso cref="System.Collections.ICollection" />
    [ComVisible(false)]
    public sealed class TreeNode<T> : ICollection<T>, ICollection
    {
        #region Fields

        internal LinkedList<TreeNode<T>> children;

        internal T item;

        internal TreeNode<T> parent;

        internal Tree<T> tree;

        private object syncRoot;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}" /> class, containing the specified value.
        /// </summary>
        /// <param name="value">The value to contain in the <see cref="TreeNode{T}" />.</param>
        public TreeNode(T value)
        {
            item = value;
            children = new LinkedList<TreeNode<T>>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the array contains ancestor <see cref="TreeNode{T}" /> s.
        /// </summary>
        /// <value>The array contains ancestor <see cref="TreeNode{T}" /> s.</value>
        public TreeNode<T>[] Ancestors
        {
            get
            {
                if (!IsLeaf) 
                    return null;
                
                var list = new List<TreeNode<T>>();
                var ancestor = parent;
                while (ancestor != null)
                {
                    list.Add(ancestor);
                    ancestor = ancestor.parent;
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// Gets the degree of this <see cref="TreeNode{T}" />, that means the total number of children nodes.
        /// </summary>
        /// <value>The degree of this <see cref="TreeNode{T}" />.</value>
        public int Degree => children.Count;

        /// <summary>
        /// Gets a value indicating whether this <see cref="TreeNode{T}" /> is leaf node of the <see cref="Tree{T}" />.
        /// </summary>
        /// <value><c>true</c> if this <see cref="TreeNode{T}" /> is leaf node; otherwise, <c>false</c>.</value>
        public bool IsLeaf => Degree == 0;

        /// <summary>
        /// Gets a value indicating whether this <see cref="TreeNode{T}" /> is root node of the <see cref="Tree{T}" />.
        /// </summary>
        /// <value><c>true</c> if this <see cref="TreeNode{T}" /> is root node; otherwise, <c>false</c>.</value>
        public bool IsRoot => parent == null;

        /// <summary>
        /// Gets the zero-based depth of the tree node in the <see cref="Tree{T}" />.
        /// </summary>
        /// <value>The zero-based depth of the tree node in the <see cref="Tree{T}" />.</value>
        public int Level
        {
            get
            {
                if (IsRoot)
                {
                    return 1;
                }

                return parent.Level + 1;
            }
        }

        /// <summary>
        /// Gets the parent <see cref="TreeNode{T}" /> of this <see cref="TreeNode{T}" />.
        /// </summary>
        /// <value>The parent <see cref="TreeNode{T}" /> of this <see cref="TreeNode{T}" />.</value>
        public TreeNode<T> Parent => parent;

        /// <summary>
        /// Gets the <see cref="Tree{T}" /> who owns this <see cref="TreeNode{T}" />.
        /// </summary>
        /// <value>The <see cref="Tree{T}" /> who owns this <see cref="TreeNode{T}" />.</value>
        public Tree<T> Tree => tree;

        /// <summary>
        /// Gets the value contained in the <see cref="TreeNode{T}" />.
        /// </summary>
        /// <value>The value contained in the <see cref="TreeNode{T}" />.</value>
        public T Value => item;

        int ICollection<T>.Count => Degree;

        int ICollection.Count => Degree;

        bool ICollection<T>.IsReadOnly => false;

        bool ICollection.IsSynchronized => false;

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
        /// Adds a new child <see cref="TreeNode{T}" /> containing the specified value.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The new child <see cref="TreeNode{T}" /> containing value.</returns>
        public TreeNode<T> AddChild(T value)
        {
            TreeNode<T> child = new TreeNode<T>(value);
            InternalInsertChildLast(child);
            return child;
        }

        /// <summary>
        /// Adds the specified new child <see cref="TreeNode{T}" />.
        /// </summary>
        /// <param name="child">The new child <see cref="TreeNode{T}" /> to add.</param>
        public void AddChild(TreeNode<T> child)
        {
            InternalInsertChildLast(child);
        }

        /// <summary>
        /// Removes all <see cref="TreeNode{T}" /> s under this <see cref="TreeNode{T}" />.
        /// </summary>
        public void Clear()
        {
            children.Clear();
        }

        /// <summary>
        /// Determines whether a child <see cref="TreeNode{T}" /> contains value is in the <see cref="TreeNode{T}" />.
        /// </summary>
        /// <param name="value">
        /// The value to locate in the children nodes of this <see cref="TreeNode{T}" />. The value can be <c>null</c> for reference types.
        /// </param>
        /// <returns><c>true</c> if <c>value</c> is found in the children nodes of this <see cref="TreeNode{T}" />; otherwise, <c>false</c>.</returns>
        public bool ContainsChild(T value)
        {
            return FindChild(value) != null;
        }

        /// <summary>
        /// Copies the children nodes to a compatible one-dimensional <see cref="System.Array" />, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array" /> that is the destination of the children nodes copied from this <see cref="TreeNode{T}" />.
        /// The <see cref="System.Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <c>array</c> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><c>array</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is less than zero..</exception>
        /// <exception cref="ArgumentException">
        /// The number of elements in the source <see cref="TreeNode{T}" /> is greater than the available space from <c>index</c> to the end of the
        /// destination <c>array</c>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "index is less than zero. ");
            }

            if (array.Length - arrayIndex < Degree)
            {
                throw new ArgumentException("The number of elements in the source TreeNode<T> is greater than the available space from index to the end of the destination array.");
            }

            var node = children.First;

            while (node != null)
            {
                array[arrayIndex++] = node.Value.Value;
                node = node.Next;
            }
        }

        /// <summary>
        /// Finds the first child <see cref="TreeNode{T}" /> that contains the specified value.
        /// </summary>
        /// <param name="value">The value to locate in the children nodes of this <see cref="TreeNode{T}" />.</param>
        /// <returns>The first child <see cref="TreeNode{T}" /> that contains the specified value, if found; otherwise, <c>null</c>.</returns>
        public TreeNode<T> FindChild(T value)
        {
            foreach (var node in children)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="IEnumerator{T}" />.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="IEnumerator{T}" />.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (TreeNode<T> child in children)
            {
                yield return child.Value;
            }
        }

        /// <summary>
        /// Removes the first occurrence of the specified value from the children nodes.
        /// </summary>
        /// <param name="value">The value to remove from the children nodes.</param>
        /// <returns>
        /// <c>true</c> if the element containing <c>value</c> is successfully removed, <c>false</c> otherwise. This method also returns <c>false</c>
        /// if <c>value</c> was not found in the children nodes.
        /// </returns>
        public bool RemoveChild(T value)
        {
            foreach (TreeNode<T> node in children)
            {
                if (node.Value.Equals(value))
                {
                    return children.Remove(node);
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the child <see cref="TreeNode{T}" /> from the <see cref="Tree{T}" />.
        /// </summary>
        /// <param name="child">The child <see cref="TreeNode{T}" /> to remove from the <see cref="Tree{T}" />.</param>
        public void RemoveChild(TreeNode<T> child)
        {
            children.Remove(child);
        }

        void ICollection<T>.Add(T item)
        {
            AddChild(item);
        }

        bool ICollection<T>.Contains(T item)
        {
            return ContainsChild(item);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Rank != 1)
            {
                throw new ArgumentException("array is multidimensional. ", nameof(array));
            }

            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException("array does not have zero-based indexing. ", nameof(array));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "index is less than zero.");
            }

            if (array.Length - index < Degree)
            {
                throw new ArgumentException("The number of elements in the source ICollection is greater than the available space from index to the end of the destination array. ", nameof(array));
            }

            if (array is T[] array2)
            {
                CopyTo(array2, index);
            }
            else
            {
                // Catch the obvious case assignment will fail. We can found all possible problems by doing the check though. For example, if the
                // element type of the Array is derived from T, we can't figure out if we can successfully copy the element beforehand.
                var targetType = array.GetType().GetElementType();
                var sourceType = typeof(T);
                var message = "The type of the source ICollection cannot be cast automatically to the type of the destination array. ";

                if (targetType != null && !(targetType.IsAssignableFrom(sourceType) || sourceType.IsAssignableFrom(targetType)))
                {
                    throw new ArgumentException(message);
                }

                if (!(array is object[] objects))
                {
                    throw new ArgumentException(message);
                }

                var node = children.First;

                try
                {
                    while (node != null)
                    {
                        objects[index++] = node.Value.Value;
                        node = node.Next;
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException(message);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool ICollection<T>.Remove(T item)
        {
            return RemoveChild(item);
        }

        internal void Invalidate()
        {
            tree = null;
            parent = null;
            children = null;
        }

        private void InternalInsertChildLast(TreeNode<T> child)
        {
            child.parent = this;
            child.tree = tree;
            children.AddLast(child);
        }

        #endregion Methods
    }
}