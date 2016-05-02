namespace ConcordanceModel.Core
{
    /// <summary>
    /// This class represents the line of text.
    /// </summary>
    /// <seealso cref="ConcordanceModel.Core.BaseEnumerable{ConcordanceModel.Core.Word}" />
   public class StringItem : BaseEnumerable<Word>
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return (base.ToString() + " ").Trim();
        }
    }
}
