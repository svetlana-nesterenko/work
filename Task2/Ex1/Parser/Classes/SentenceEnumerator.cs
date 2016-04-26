using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Interfaces;

namespace Parser.Classes
{
    class SentenceEnumerator : IEnumerator<ISentence>
    {
        private Paragraph _collection;
        private int _curIndex;
        private ISentence _curItem;

        public SentenceEnumerator(Paragraph collection)
        {
            _collection = collection;
            _curIndex = -1;
            _curItem = default(ISentence);
        }

        public ISentence Current
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
