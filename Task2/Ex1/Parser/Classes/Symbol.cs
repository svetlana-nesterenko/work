using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Classes
{
    public abstract class Symbol
    {
        private char _char;
        public char Char
        {
            get { return _char; }
            private set { _char = value; }
        }

        public Symbol(char c)
        {
            this._char = c;
        }
    }
}
