using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Interfaces;

namespace Parser.Classes
{
    public class Text : ICollection<IParagraph>
    {
        /// <summary>
        /// The items.
        /// </summary>
        private List<IParagraph> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        public Text()
        {
            _items = new List<IParagraph>();
        }

        #region Implementation of ICollection
        /// <summary>
        /// Gets or sets the <see cref="IItem"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IItem"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public IParagraph this[int index]
        {
            get { return (IParagraph)_items[index]; }
            set { _items[index] = value; }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <exception cref="System.Exception">Reached the maximum weight of the current gift (2000g).</exception>
        public void Add(IParagraph item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public bool Contains(IParagraph item)
        {
            bool found = false;
            foreach (IParagraph i in _items)
            {
                if (i.Equals(item))
                {
                    found = true;
                }
            }
            return found;
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        /// <exception cref="System.ArgumentNullException">The array cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The starting array index cannot be negative.</exception>
        /// <exception cref="System.ArgumentException">The destination array has fewer elements than the collection.</exception>
        public void CopyTo(IParagraph[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < _items.Count; i++)
            {
                array[i + arrayIndex] = _items[i];
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(IParagraph item)
        {
            bool result = false;
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    _items.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IParagraph> GetEnumerator()
        {
            return new ParagraphEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ParagraphEnumerator(this);
        }
        #endregion
    }
}
