using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Classes
{
    public class ImperativeSentence: Sentence
    {
        public ImperativeSentence() : base()
        {
            base._lastSign = new PunctuationSign("!");
        }
    }
}
