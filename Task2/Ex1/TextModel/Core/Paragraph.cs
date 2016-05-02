namespace TextModel.Core
{
    /// <summary>
    /// Class represents the part of the text (paragraph) which consists from sentences.
    /// </summary>
    /// <seealso cref="TextModel.Core.BaseEnumerable{TextModel.Core.Sentence}" />
    public class Paragraph : BaseEnumerable<Sentence>
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString() + "\r\n";
        }
    }
}
