namespace Parser.Classes
{
    /// <summary>
    /// This class represents the sentences wich end with "!"
    /// </summary>
    /// <seealso cref="Parser.Classes.Sentence" />
    public class ImperativeSentence: Sentence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImperativeSentence"/> class.
        /// </summary>
        public ImperativeSentence() : base()
        {
            base._lastSign = new PunctuationSign("!");
        }
    }
}
