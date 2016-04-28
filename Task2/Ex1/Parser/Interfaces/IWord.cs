namespace Parser.Interfaces
{
    #region Usings

    using System.Collections.Generic;
    using Parser.Classes;

    #endregion
    /// <summary>
    /// Interface used for the symbol or numeric part of sentence.
    /// </summary>
    /// <seealso cref="Parser.Interfaces.ISentenceItem" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{Parser.Classes.Symbol}" />
    public interface IWord: ISentenceItem, IEnumerable<Symbol>
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
       
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        int Length { get; }
    }
}
