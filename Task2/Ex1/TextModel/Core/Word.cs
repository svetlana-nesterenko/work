namespace TextModel.Core
{
    #region Usings

    using System;
    using System.Linq;

    #endregion

    /// <summary>
    /// Class represents the symbol part of sentence.
    /// </summary>
    /// <seealso cref="TextModel.Core.BaseEnumerable{TextModel.Core.Symbol}" />
    /// <seealso cref="TextModel.Core.IWord" />
    public class Word : BaseEnumerable<Symbol>, IWord
    {
        /// <summary>
        /// Gets the <see cref="Symbol"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Symbol"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Symbol this[int index]
        {
            get { return _Items[index]; }
        }

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
            return String.Join("", _Items.Select(i => i.Value));
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
