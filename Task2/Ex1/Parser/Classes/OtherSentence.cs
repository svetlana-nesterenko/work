using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Classes
{
    public class OtherSentence : Sentence
    {
        public void SetLastSign(PunctuationSign item)
        {
            base._lastSign = item;
        }
    }
}
