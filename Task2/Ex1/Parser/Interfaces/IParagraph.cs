namespace Parser.Interfaces
{
    #region Usings

    using System.Collections.Generic;
    
    #endregion

    /// <summary>
    /// Interface used for paragraphs of the text.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.ICollection{Parser.Interfaces.ISentence}" />
    public interface IParagraph : ICollection<ISentence>
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        List<ISentence> Items { get; }
    }
}
