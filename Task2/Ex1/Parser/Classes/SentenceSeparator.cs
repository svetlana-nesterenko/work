using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Interfaces;

namespace Parser.Classes
{
    public class SentenceSeparator: IPunctuation
    {
        private Symbol value;
        public Symbol Value
        {
            get { return this.value; }
        }

        public string Chars
        {
            get { return ""; }
        }
        public SentenceSeparator (string chars)
        {
            //this.value = new Symbol(chars);
        }
    }
}
