using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TextModel.Core
{
    public abstract class BaseEnumerable<T> : IBaseEnumerable<T>
    {
        protected T[] _Items;

        public BaseEnumerable()
        {
            _Items = new T[0];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int index = 0; index < _Items.Length; index++)
            {
                yield return _Items[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        public void Add(T element)
        {
            Array.Resize(ref _Items, _Items.Length + 1);
            _Items[_Items.Length - 1] = element;
        }

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

        public void Clear()
        {
            _Items = new T[0];
        }

        public int Count
        {
            get { return _Items.Length; }
        }

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _Items)
            {
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }
    }
}
