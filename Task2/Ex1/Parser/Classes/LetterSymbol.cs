using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Classes
{
    public class LetterSymbol: Symbol
    {
        private string _vowelSymbols = "aeiouy";

        public LetterSymbol(char c)
            : base(c)
        {
            this.LetterTypes = _vowelSymbols.Contains(String.Format("{0}", c).ToLower()) ? LetterTypes.vowel : LetterTypes.consonant;
        }

        public LetterTypes LetterTypes { get; private set; }
    }
}
