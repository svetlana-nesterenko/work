namespace ConcordanceModel
{
    #region Usings
    
    using System.Collections.Generic;
 
    #endregion

    /// <summary>
    /// Class represents the elements of concordance.
    /// </summary>
    public class ConcordanceItem
    {
        #region Private Fields

        /// <summary>
        /// The _ page indexes
        /// </summary>
        private readonly List<int> _PageIndexes;
        
        /// <summary>
        /// The _ count
        /// </summary>
        private int _Count;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcordanceItem"/> class.
        /// </summary>
        public ConcordanceItem()
        {
            _PageIndexes = new List<int>();
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the occurrence.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        public void AddOccurrence(int pageIndex)
        {
            if (!_PageIndexes.Contains(pageIndex))
            {
                _PageIndexes.Add(pageIndex);
            }
            _Count++;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get
            {
                return _Count;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the page indexes.
        /// </summary>
        /// <value>
        /// The page indexes.
        /// </value>
        public int[] PageIndexes
        {
            get { return _PageIndexes.ToArray(); }
        }
        
        #endregion
    }
}
