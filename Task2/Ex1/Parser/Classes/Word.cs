﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Interfaces;

namespace Parser.Classes
{
    public class Word: IWord
    {
        private Symbol[] symbols;

        public Word(string chars)
        {
            if (chars != null)
            {
                List<Symbol> list = new List<Symbol>();
                foreach (char c in chars)
                {
                    if (c >= 48 && c <= 57)
                    {
                        list.Add(new NumericSymbol(c));
                    }
                    else
                    {
                        list.Add(new LetterSymbol(c));
                    }
                }
                this.symbols = list.ToArray();
            }
            else
            {
                this.symbols = null;
            }
        }

        public Symbol this[int index]
        {
            get { return this.symbols[index]; }
        }

        public int Length
        {
            get { return (symbols != null) ? symbols.Length : 0; }
        }

        public string Chars
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var s in this.symbols)
                {
                    sb.Append(s.Char);
                }
                return sb.ToString();
            }
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            return symbols.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.symbols.GetEnumerator();
        }
    }
}
