namespace Parser.Classes
{
    #region Usings

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using Parser.Interfaces;

    #endregion

    /// <summary>
    /// This class represents the sentence which consists from sentence items collection (words, punctuation sign).
    /// </summary>
    /// <seealso cref="Parser.Interfaces.ISentence" />
    /// <seealso cref="System.Collections.Generic.ICollection{Parser.Interfaces.ISentenceItem}" />
    public abstract class Sentence : ISentence
    {
        #region Private Fields

        /// <summary>
        /// The _items.
        /// </summary>
        private List<ISentenceItem> _items;

        #endregion

        #region Protected Fields

        /// <summary>
        /// Gets or sets the _last sign of sentence.
        /// </summary>
        /// <value>
        /// The _last sign.
        /// </value>
        protected PunctuationSign _lastSign { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<ISentenceItem> Items
        {
            get { return _items; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Sentence"/> class.
        /// </summary>
        public Sentence()
        {
            _items = new List<ISentenceItem>();
        }
        
        #endregion
       
        #region Implementation of ICollection

        /// <summary>
        /// Gets or sets the <see cref="ISentenceItem"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="ISentenceItem"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public ISentenceItem this[int index]
        {
            get { return (ISentenceItem)_items[index]; }
            set { _items[index] = value; }
        }
        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(ISentenceItem item)
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
        public bool Contains(ISentenceItem item)
        {
            bool found = false;
            foreach (ISentenceItem i in _items)
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
        public void CopyTo(ISentenceItem[] array, int arrayIndex)
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
        public bool Remove(ISentenceItem item)
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
        public IEnumerator<ISentenceItem> GetEnumerator()
        {
            for (int index = 0; index < _items.Count; index++)
            {
                yield return _items[index];
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
            for (int index = 0; index < _items.Count; index++)
            {
                yield return _items[index];
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the sentance.
        /// </summary>
        /// <returns></returns>
        public string CreateSentence()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _items)
            {
                if (item is IWord && sb.Length != 0) 
                {
                    sb.Append(" ");
                }
                sb.Append(item.Chars);
            }
            //if (_lastSign != null)
            //{
            //    sb.Append(_lastSign.Chars); 
            //}
            return sb.ToString();
        }

        public string FormatSentence()
        {
            const string spacesAfter = ",-:')}]>";
            const string spacesAfter2 = "=";
            const string spacesBefore = "({[<=";

            const string punctuationAfterPunctuatuation = "-=+>\"'()[]{}";
            const string punctuationAfterPunctuatuation2 = "-=+>\"'";

            const string allowedEnd = "!?.\";';);";

            StringBuilder sb = new StringBuilder();
            ISentenceItem lastItem = null;
            bool quoteExists = false;
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];

                if (item is IWord)
                {
                    if (sb.Length != 0 && (lastItem is IWord || (lastItem is IPunctuation && (spacesAfter.IndexOf(lastItem.Chars[0]) > -1) || spacesAfter2.IndexOf(lastItem.Chars[0]) > -1)))
                    {
                        sb.Append(" ");
                    }
                    sb.Append(item.Chars);
                }
                else
                {
                    if (lastItem is IWord)
                    {
                        if (spacesAfter.IndexOf(item.Chars) == -1 && i < _items.Count - 1)
                        {
                            if (item.Chars.Equals("\"") && !quoteExists || !item.Chars.Equals("\""))
                            {
                                sb.Append(" ");
                            }
                            if (item.Chars.Equals("\""))
                            {
                                quoteExists = !quoteExists;
                            }
                        }
                        if (i < _items.Count - 1)
                        {
                            sb.Append(item.Chars);
                        }
                    }
                    else if (lastItem is IPunctuation)
                    {
                        if ((spacesBefore.IndexOf(lastItem.Chars) == -1 || spacesAfter2.IndexOf(lastItem.Chars) > -1) && spacesAfter.IndexOf(lastItem.Chars) > -1 && i < _items.Count - 1
                            && (item.Chars.Equals("\"") && !quoteExists || !item.Chars.Equals("\"")))
                        {
                            sb.Append(" ");
                        }
                        if (item.Chars.Equals("\""))
                        {
                            quoteExists = !quoteExists;
                        }
                        if (i < _items.Count - 1)
                        {
                            sb.Append(item.Chars);
                        }
                    }
                }

                lastItem = item;
            }

            if (allowedEnd.IndexOf(GetLastSign().Chars) > -1 || GetLastSign().Chars.Equals("..."))
            {
                sb.Append(GetLastSign().Chars);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the last sign.
        /// </summary>
        /// <returns></returns>
        public PunctuationSign GetLastSign()
        {
            return _lastSign;
        }

        #endregion
    }
}
