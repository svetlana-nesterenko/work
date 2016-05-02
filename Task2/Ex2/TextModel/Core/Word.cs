namespace ConcordanceModel.Core
{
    #region Usings

    using System;
    using System.Linq;
    
    #endregion

    /// <summary>
    /// Class represents the symbol part of sentence.
    /// </summary>
    /// <seealso cref="ConcordanceModel.Core.BaseEnumerable{ConcordanceModel.Core.Symbol}" />
    public class Word : BaseEnumerable<Symbol>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Word"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Word(string value)
        {
            foreach (char c in value)
            {
                Add(new Symbol(c.ToString()));
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
            return String.Join("", _Items.Select(i => i.Value)) + " ";
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return _Items.Select(i => i.Length).Sum();
        }
        
        #endregion
    }
}
