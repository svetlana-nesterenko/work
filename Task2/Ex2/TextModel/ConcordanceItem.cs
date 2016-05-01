using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordanceModel
{
    public class ConcordanceItem
    {
        private List<int> _PageIndexes;
        private int _Count;
        public ConcordanceItem()
        {
            _PageIndexes = new List<int>();
        }

        public void AddOccurrence(int pageIndex)
        {
            if (!_PageIndexes.Contains(pageIndex))
            {
                _PageIndexes.Add(pageIndex);
            }
            _Count++;
        }

        public int Count
        {
            get
            {
                return _Count;
            }
        }

        public int[] PageIndexes
        {
            get { return _PageIndexes.ToArray(); }
        }
    }
}
