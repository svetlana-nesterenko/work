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

        public string Value
        {
            get { return String.Join("", _Items.Select(i => i.Value)); }
        }

        public int GetLength()
        {
            return _Items.Length;
        }

        public int Position { get; private set; }
    }
}
