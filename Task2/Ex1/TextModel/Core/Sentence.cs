namespace TextModel.Core
{
    #region Usings

    using System.Linq;
    using TextModel.Enum;

    #endregion

    /// <summary>
    /// This class represents the sentence which consists from sentence items (words, punctuation sign).
    /// </summary>
    /// <seealso cref="TextModel.Core.BaseEnumerable{TextModel.Core.ISentenceItem}" />
    public class Sentence : BaseEnumerable<ISentenceItem>
    {
        #region Public Methods

        /// <summary>
        /// Gets the type of the sentence.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString() + " ";
        }

        /// <summary>
        /// Gets the last item.
        /// </summary>
        /// <returns></returns>
        public ISentenceItem GetLastItem()
        {
            return _Items.LastOrDefault();
        }
        
        #endregion
    }
}
