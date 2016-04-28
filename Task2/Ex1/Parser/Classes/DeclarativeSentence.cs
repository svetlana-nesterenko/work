namespace Parser.Classes
{
    /// <summary>
    /// This class represents the sentences wich end with "."
    /// </summary>
    /// <seealso cref="Parser.Classes.Sentence" />
   public class DeclarativeSentence: Sentence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeclarativeSentence"/> class.
        /// </summary>
        public DeclarativeSentence(): base()
        {
            base._lastSign = new PunctuationSign(".");
        }
    }
}
