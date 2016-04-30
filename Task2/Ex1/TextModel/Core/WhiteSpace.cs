using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextModel.Core
{
    public class WhiteSpace : ISentenceItem
    {
        private string _WhiteSpace;
        public WhiteSpace(string s)
        {
            _WhiteSpace = s;
        }

        public override string ToString()
        {
            return _WhiteSpace;
        }

        public int GetLength()
        {
            return _WhiteSpace.Length;
        }
    }
}
