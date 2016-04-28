namespace Parser.Interfaces
{
    /// <summary>
    /// Interface used for defined part of sentence (word, punctuation sign).
    /// </summary>
    public interface ISentenceItem
    {
        /// <summary>
        /// Gets the chars.
        /// </summary>
        /// <value>
        /// The chars.
        /// </value>
        string Chars { get; }
    }
}
