using System;
using System.ComponentModel;
using System.Linq;
using ConcordanceModel.Core;

namespace ConcordanceModel.Core
{
    public class Word : BaseEnumerable<Symbol>
    {
        public Word(string value)
        {
            foreach (char c in value)
            {
                Add(new Symbol(c.ToString()));
            }
        }

        public override string ToString()
        {
            return String.Join("", _Items.Select(i => i.Value)) + " ";
        }

        public int GetLength()
        {
            return _Items.Select(i => i.Length).Sum();
        }
    }
}
