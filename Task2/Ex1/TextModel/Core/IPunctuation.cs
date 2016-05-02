namespace TextModel.Core
{
    /// <summary>
    /// Interface used for the punctuation part of sentence.
    /// </summary>
    /// <seealso cref="TextModel.Core.ISentenceItem" />
    public interface IPunctuation : ISentenceItem
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        string Value { get; }
    }
}
