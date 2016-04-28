namespace Parser.Classes
{
    /// <summary>
    /// This class represents the sentences wich end with "?"
    /// </summary>
    /// <seealso cref="Parser.Classes.Sentence" />
    public class InterrogativeSentence: Sentence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterrogativeSentence"/> class.
        /// </summary>
        public InterrogativeSentence(): base()
        {
            base._lastSign = new PunctuationSign("?");
        }
    }
}
