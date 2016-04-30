using System;
using System.Linq;

namespace TextModel.Core
{
    public class Word : BaseEnumerable<Symbol>, IWord
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
            return String.Join("", _Items.Select(i => i.Value));
        }

        public int GetLength()
        {
            return _Items.Select(i => i.Length).Sum();
        }
    }
}
