using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Interfaces;

namespace Parser.Classes
{
    public class Paragraph : IParagraph, ICollection<ISentence>
    {
        private List<ISentence> _items;

        public double Indent { get; set; }
        public double LineSpacing { get; set; }

        public Paragraph()
        {
            _items = new List<ISentence>();
        }

        public List<ISentence> Items
        {
            get { return _items; }
        }

        #region Implementation of ICollection
        public ISentence this[int index]
        {
            get { return (ISentence)_items[index]; }
            set { _items[index] = value; }
        }

        public void Add(ISentence item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(ISentence item)
        {
            bool found = false;
            foreach (ISentence i in _items)
            {
                if (i.Equals(item))
                {
                    found = true;
                }
            }
            return found;
        }

        public void CopyTo(ISentence[] array, int arrayIndex)
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

        public bool Remove(ISentence item)
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

        public IEnumerator<ISentence> GetEnumerator()
        {
            return new SentenceEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SentenceEnumerator(this);
        }
        #endregion
    }
}
