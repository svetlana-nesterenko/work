namespace NewYearsGift.Classes
{
    #region Usings

    using System.Collections;
    using System.Collections.Generic;
    using NewYearsGift.Interfaces;

    #endregion

    class ItemEnumerator : IEnumerator<IItem>
    {
        private Gift _collection;
        private int _curIndex;
        private IItem _curItem;
        
        public ItemEnumerator(Gift collection)
        {
            _collection = collection;
            _curIndex = -1;
            _curItem = default(IItem);

        }
        public IItem Current
        {
            get { return _curItem; }
        }

        public void Dispose()
        {

        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (++_curIndex >= _collection.Count)
            {
                return false;
            }
            else
            {

                _curItem = _collection[_curIndex];
            }
            return true;
        }

        public void Reset()
        {
            _curIndex = -1; 
        }
    }
}
