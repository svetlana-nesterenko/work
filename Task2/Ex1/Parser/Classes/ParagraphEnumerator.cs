using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Interfaces;

namespace Parser.Classes
{
    class ParagraphEnumerator : IEnumerator<IParagraph>
    {
        private Text _collection;
        private int _curIndex;
        private IParagraph _curItem;

        public ParagraphEnumerator(Text collection)
        {
            _collection = collection;
            _curIndex = -1;
            _curItem = default(IParagraph);
        }

        public IParagraph Current
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
