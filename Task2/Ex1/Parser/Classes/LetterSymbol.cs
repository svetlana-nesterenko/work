namespace Parser.Classes
{
    #region Usings

    using System;
    
    #endregion

    /// <summary>
    /// Class used for representing the literal symbols.
    /// </summary>
    /// <seealso cref="Parser.Classes.Symbol" />
    public class LetterSymbol: Symbol
    {
        /// <summary>
        /// The _vowel symbols.
        /// </summary>
        private string _vowelSymbols = "aeiouy";

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterSymbol"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public LetterSymbol(char c)
            : base(c)
        {
            this.LetterType = _vowelSymbols.Contains(String.Format("{0}", c).ToLower()) ? LetterTypes.vowel : LetterTypes.consonant;
        }

        /// <summary>
        /// Gets the letter types.
        /// </summary>
        /// <value>
        /// The letter types.
        /// </value>
        public LetterTypes LetterType { get; private set; }
    }
}
