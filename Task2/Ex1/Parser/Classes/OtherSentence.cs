namespace Parser.Classes
{
    /// <summary>
    /// This class represents the sentences wich not end with "." or "?" or "!"
    /// </summary>
    /// <seealso cref="Parser.Classes.Sentence" />
    public class OtherSentence : Sentence
    {
        public OtherSentence() : base()
        {
            base._lastSign = new PunctuationSign("");
        }

        /// <summary>
        /// Sets the last sign.
        /// </summary>
        /// <param name="item">The item.</param>
        public void SetLastSign(PunctuationSign item)
        {
            if (item != null)
            {
                base._lastSign = item;
            }
        }
    }
}
