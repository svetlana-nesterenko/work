namespace TextModel.Core
{
    /// <summary>
    /// Interface used for defined part of sentence (word, punctuation sign, white spase).
    /// </summary>
    public interface ISentenceItem
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <returns></returns>
        int GetLength();
    }
}
