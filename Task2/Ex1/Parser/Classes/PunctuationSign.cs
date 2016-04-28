namespace Parser.Classes
{
    #region Usings

    using Parser.Interfaces;

    #endregion

    /// <summary>
    /// Class represents the punctuation part of sentence.
    /// </summary>
    /// <seealso cref="Parser.Interfaces.IPunctuation" />
    public class PunctuationSign: IPunctuation
    {
        /// <summary>
        /// Gets the chars.
        /// </summary>
        /// <value>
        /// The chars.
        /// </value>
        public string Chars { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PunctuationSign"/> class.
        /// </summary>
        /// <param name="chars">The chars.</param>
        public PunctuationSign (string chars)
        {
            this.Chars = chars;
        }

    }
}
