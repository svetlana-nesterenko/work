using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parser.Interfaces;

namespace Parser.Classes
{
    class SentenceItemEnumerator : IEnumerator<ISentenceItem>
    {
        private Sentence _collection;
        private int _curIndex;
        private ISentenceItem _curItem;

        public SentenceItemEnumerator(Sentence collection)
        {
            _collection = collection;
            _curIndex = -1;
            _curItem = default(ISentenceItem);
        }

        public ISentenceItem Current
        {
            get { return _curItem; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        object System.Collections.IEnumerator.Current
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
