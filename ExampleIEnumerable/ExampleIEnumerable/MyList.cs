using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace ExampleIEnumerable
{
    /// <summary>
    /// My Collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyList<T> : IEnumerable<T>, ICollection<T>
    {
        private T[] _List;

        public MyList()
        {
            _List = new T[0];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnum<T>(_List);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        /// <summary>
        /// Adds new element to the collection.
        /// </summary>
        /// <param name="element">Element</param>
        public void Add(T element)
        {
            Array.Resize(ref _List, _List.Length + 1);
            _List[_List.Length - 1] = element;
        }

        /// <summary>
        /// Clears all items in the collection.
        /// </summary>
        public void Clear()
        {
            _List = new T[0];
        }

        /// <summary>
        /// Returns true if the collection contains specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return _List.Contains(item);
        }

        /// <summary>
        /// Copies the collection to the passed array
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="arrayIndex">Start index in the destination array</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            Array.Copy(_List, 0, array, arrayIndex, _List.Length);
        }

        /// <summary>
        /// Returns length of the collection.
        /// </summary>
        public int Count
        {
            get { return _List.Length; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the item from the collection.
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if the item was removed</returns>
        public bool Remove(T item)
        {
            for (int i = 0; i < _List.Length; i++)
            {
                if (_List[i].Equals(item))
                {
                    T[] Array2 = new T[_List.Length - 1];
                    Array.Copy(_List, Array2, i);
                    Array.Copy(_List, i + 1, Array2, i, _List.Length - i - 1);
                    _List = Array2;
                    return true;
                }
            }
            return false;
        }
    }
}
