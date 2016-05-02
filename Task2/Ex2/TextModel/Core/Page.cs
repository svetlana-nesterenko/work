namespace ConcordanceModel.Core
{
    /// <summary>
    /// Class represents the part of the text (pages) which consists from string items.
    /// </summary>
    /// <seealso cref="ConcordanceModel.Core.BaseEnumerable{ConcordanceModel.Core.StringItem}" />
    public class Page : BaseEnumerable<StringItem>
    {
        /// <summary>
        /// The _ page index
        /// </summary>
        private readonly int _PageIndex;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        public Page(int pageIndex)
        {
            _PageIndex = pageIndex;
        }

        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
        }
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
