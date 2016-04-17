namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using NewYearsGift.Interfaces;
    using System.Linq;

    #endregion

    /// <summary>
    /// This class represents the New Year's gift which consists from IItems collection (toys, candies and etc.).
    /// </summary>
    /// <seealso cref="System.Collections.Generic.ICollection{NewYearsGift.Interfaces.IItem}" />
    public class Gift : ICollection<IItem>
    {
        #region Private Fields

        /// <summary>
        /// The _items
        /// </summary>
        private List<IItem> _items;

        /// <summary>
        /// The _weight
        /// </summary>
        private double _weight;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the gift.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product article.
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        public string ProductArticle { get; set; }

        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight
        {
            get { return _weight; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Gift"/> class.
        /// </summary>
        public Gift()
        {
            _items = new List<IItem>();
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Sorts the name of the items by.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IItem> SortItemsByName()
        {
            return _items.OrderBy(i => i.Name).ToArray();
        }

        /// <summary>
        /// Finds items with specified range of sugar content.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public IEnumerable<IItem> FindBySugarContent(double min, double max)
        {
            return _items.Where(i => i is IHasSugar).Cast<IHasSugar>().Where(c => c.SugarContent >= min && c.SugarContent <= max).Cast<IItem>().ToArray();
        }

        /// <summary>
        /// Recalculates the weight.
        /// </summary>
        public void RecalculateWeight()
        {
            _weight = _items.Select(i => i.Weight).Sum();
        }

        #endregion

        #region Implementation of ICollection

        /// <summary>
        /// Gets or sets the <see cref="IItem"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IItem"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public IItem this[int index]
        {
            get { return (IItem)_items[index]; }
            set { _items[index] = value; }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <exception cref="System.Exception">Reached the maximum weight of the current gift (2000g).</exception>
        public void Add(IItem item)
        {
            this.RecalculateWeight();
            if (this.Weight > 2000)
            {
                throw new Exception("Reached the maximum weight of the current gift (2000g).");
            }
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
        public bool Contains(IItem item)
        {
            bool found = false;
            foreach (IItem i in _items)
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
        public void CopyTo(IItem[] array, int arrayIndex)
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
        public bool Remove(IItem item)
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
        public IEnumerator<IItem> GetEnumerator()
        {
            return new ItemEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ItemEnumerator(this);
        }

        #endregion
    }
}
