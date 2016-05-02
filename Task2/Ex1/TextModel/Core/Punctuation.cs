namespace TextModel.Core
{
    /// <summary>
    /// Class represents the punctuation part of sentence.
    /// </summary>
    /// <seealso cref="TextModel.Core.IPunctuation" />
    public class Punctuation :  IPunctuation
    {
        /// <summary>
        /// The _ symbol
        /// </summary>
        private Symbol _Symbol;

        /// <summary>
        /// Initializes a new instance of the <see cref="Punctuation"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        public Punctuation(string p)
        {
            _Symbol = new Symbol(p);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value
        {
            get
            {
                return _Symbol.Value.ToString();
            }
        }

        #region Public Methods
        
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _Symbol.Value.ToString();
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return _Symbol.Length;
        }
        
        #endregion
    }
}
