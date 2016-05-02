namespace ConcordanceModel.Core
{
    #region Usings

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    #endregion

    /// <summary>
    /// Class used for representing base fields and methods for different classes (paragraph, sentence and etc.)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public abstract class BaseEnumerable<T> : IEnumerable<T>
    {
        #region Protected Fields

        /// <summary>
        /// The _ items
        /// </summary>
        protected T[] _Items;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEnumerable{T}"/> class.
        /// </summary>
        public BaseEnumerable()
        {
            _Items = new T[0];
        }

        #endregion

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < _Items.Length; index++)
            {
                yield return _Items[index];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public void Add(T element)
        {
            Array.Resize(ref _Items, _Items.Length + 1);
            _Items[_Items.Length - 1] = element;
        }

        /// <summary>
        /// Inserts the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="index">The index.</param>
        /// <exception cref="System.IndexOutOfRangeException">Index is out of range.</exception>
        public void Insert(T element, int index)
        {
            if (index >= _Items.Length)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            Array.Resize(ref _Items, _Items.Length + 1);
            for (int i = _Items.Length - 1; i > index; i--)
            {
                _Items[i] = _Items[i - 1];
            }

            _Items[index] = element;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _Items = new T[0];
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return _Items.Length; }
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            for (int i = 0; i < _Items.Length; i++)
            {
                if (_Items[i].Equals(item))
                {
                    T[] Array2 = new T[_Items.Length - 1];
                    Array.Copy(_Items, Array2, i);
                    Array.Copy(_Items, i + 1, Array2, i, _Items.Length - i - 1);
                    _Items = Array2;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _Items)
            {
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }
        
        #endregion
    }
}
