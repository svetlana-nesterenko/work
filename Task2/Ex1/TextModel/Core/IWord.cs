namespace TextModel.Core
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Interface used for the symbol part of sentence.
    /// </summary>
    /// <seealso cref="TextModel.Core.ISentenceItem" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{TextModel.Core.Symbol}" />
    public interface IWord : ISentenceItem, IEnumerable<Symbol>
    {
        /// <summary>
        /// Gets the <see cref="Symbol"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Symbol"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        Symbol this[int index] { get; }
    }
}
