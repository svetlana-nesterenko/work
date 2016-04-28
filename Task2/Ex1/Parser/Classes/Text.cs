namespace Parser.Classes
{
    #region Usings

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Parser.Interfaces;

    #endregion

    /// <summary>
    /// This class represents the text which consist from paragraphs collection.
    /// </summary>
    /// <seealso cref="IParagraph" />
    public class Text : ICollection<IParagraph>
    {

        /// <summary>
        /// The _items.
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
        /// Gets or sets the <see cref="IParagraph"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IParagraph"/>.
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
        /// Sorts all sentences of the text in ascending order of the number of words in each of them.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ISentence> SortSentencesByWordCount()
        {
            return _items.Select(p => p.ToArray()).SelectMany(s => s.ToArray()).OrderBy(i => { return i.ToArray().Count(w => w is Word); });
        }

        /// <summary>
        /// Finds the words in interrogative sentences by a given length.
        /// </summary>
        /// <param name="lenght">The lenght.</param>
        /// <returns></returns>
        public IEnumerable<string> FindWordsInInterrogativeSentences(int lenght)
        {
            return _items.Select(p => p.ToArray()).SelectMany(s => s.ToArray()).Where(s => s is InterrogativeSentence).SelectMany(i => i.ToArray()).
                Where(i => i is IWord).Cast<IWord>().Where(w => w.Length == lenght).Select(s => s.Chars).Distinct().ToArray();
        }

        public string DeleteWordsByLenght(int length, LetterTypes letterType)
        {
           foreach (var paragraph in _items)
           {
               foreach (var sentence in paragraph)
               {
                   foreach (var item in sentence)
                   {
                       if (item is IWord && item.Chars.Length == length && ((IWord)item)[0] is LetterSymbol &&
                           ((LetterSymbol) ((IWord) item)[0]).LetterType == LetterTypes.consonant)
                       {
                           sentence.Remove(item);    
                       }
                   }
               }
           }
           return this.GetText();
        }

        public string Replace(int length, string substring)
        {
            foreach (var paragraph in _items)
            {
                foreach (var sentence in paragraph)
                {
                    foreach (ISentenceItem item in sentence)
                    {
                        if (item is IWord && item.Chars.Length == length)
                        {
                            ((Word)item).Chars = substring;
                        }
                    }
                }
            }
            return this.GetText();
        }

        public string GetText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var paragraph in _items)
            {
                foreach (var sentence in paragraph)
                {
                    stringBuilder.Append(sentence.CreateSentence());
                    stringBuilder.Append(" ");
                }
                stringBuilder.AppendLine("");
            }
            return stringBuilder.ToString();
        }

        public string FormatText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var paragraph in _items)
            {
                foreach (var sentence in paragraph)
                {
                    stringBuilder.Append(sentence.FormatSentence());
                    
                    stringBuilder.Append(" ");
                }
                stringBuilder.AppendLine("");
            }
            return stringBuilder.ToString();
        }

        #endregion
    }
}
