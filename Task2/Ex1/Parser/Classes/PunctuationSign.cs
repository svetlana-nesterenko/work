using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Interfaces;

namespace Parser.Classes
{
    public class PunctuationSign: IPunctuation
    {
        public PunctuationSign (string chars)
        {
            this.Chars = chars;
        }

        public string Chars { get; private set; }
    }
}
