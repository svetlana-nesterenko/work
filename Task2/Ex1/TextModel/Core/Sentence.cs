using System.Linq;
using System.Text;
using TextModel.Enum;

namespace TextModel.Core
{
    public class Sentence : BaseEnumerable<ISentenceItem>
    {
        public SentenceTypeEnum GetSentenceType()
        {
            ISentenceItem lastItem = GetLastItem();
            if (lastItem is IWord ||
                (lastItem is IPunctuation &&
                 (((IPunctuation) lastItem).Value.Equals(".") || ((IPunctuation) lastItem).Value.Equals("..."))))
            {
                return SentenceTypeEnum.Declarative;
            }
            
            if (lastItem is IPunctuation && ((IPunctuation) lastItem).Value.Equals("!"))
            {
                return SentenceTypeEnum.Imperative;
            }
            
            if (lastItem is IPunctuation && ((IPunctuation) lastItem).Value.Equals("?"))
            {
                return SentenceTypeEnum.Interrogative;
            }

            return SentenceTypeEnum.Declarative;
        }

        public override string ToString()
        {
            return base.ToString() + " ";
        }

        public ISentenceItem GetLastItem()
        {
            return _Items.LastOrDefault();
        }
    }
}
