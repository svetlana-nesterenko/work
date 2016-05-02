namespace TextModel.Core
{
    /// <summary>
    /// Class represents the word separating part of sentence.
    /// </summary>
    /// <seealso cref="TextModel.Core.ISentenceItem" />
    public class WhiteSpace : ISentenceItem
    {
        /// <summary>
        /// The _ white space
        /// </summary>
        private string _WhiteSpace;
        public WhiteSpace(string s)
        {
            _WhiteSpace = s;
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
            return _WhiteSpace;
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return _WhiteSpace.Length;
        }
        
        #endregion
    }
}
