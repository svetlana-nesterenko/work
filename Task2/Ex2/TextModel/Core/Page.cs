namespace ConcordanceModel.Core
{
    /// <summary>
    /// Class represents the part of the text (paragraph) which consists from sentences.
    /// </summary>
    /// <seealso cref="ConcordanceModel.Core.BaseEnumerable{ConcordanceModel.Core.StringItem}" />
    public class Page : BaseEnumerable<StringItem>
    {
        private readonly int _PageIndex;
        public Page(int pageIndex)
        {
            _PageIndex = pageIndex;
        }

        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
        }
        public override string ToString()
        {
            return base.ToString() + "\r\n";
        }
    }
}
