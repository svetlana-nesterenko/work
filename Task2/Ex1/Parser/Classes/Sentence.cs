using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Interfaces;

namespace Parser.Classes
{
    public abstract class Sentence : ISentence, ICollection<ISentenceItem>
    {
        private List<ISentenceItem> _items;
        protected PunctuationSign _lastSign { get; set; }

        public Sentence()
        {
            _items = new List<ISentenceItem>();
        }

        public List<ISentenceItem> Items
        {
            get { return _items; }
        }

        #region Implementation of ICollection
        public ISentenceItem this[int index]
        {
            get { return (ISentenceItem)_items[index]; }
            set { _items[index] = value; }
        }
        public void Add(ISentenceItem item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

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

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

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

        public IEnumerator<ISentenceItem> GetEnumerator()
        {
            return new SentenceItemEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SentenceItemEnumerator(this);
        }
        #endregion

        public string CreateSentance()
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
            if (_lastSign != null)
            {
                sb.Append(_lastSign.Chars); 
            }
            return sb.ToString();
        }

        public PunctuationSign GetLastSign()
        {
            return _lastSign;
        }
    }
}
