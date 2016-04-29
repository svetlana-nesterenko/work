namespace Parser.Interfaces
{
    #region Usings

    using System.Collections.Generic;
    
    #endregion

    /// <summary>
    /// Interface used for sentences.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.ICollection{Parser.Interfaces.ISentenceItem}" />
    public interface ISentence: ICollection<ISentenceItem>
    {
        List<ISentenceItem> Items { get; }

        /// <summary>
        /// Creates the sentance.
        /// </summary>
        /// <returns></returns>
        string CreateSentence();

        string FormatSentence();
    }
}
