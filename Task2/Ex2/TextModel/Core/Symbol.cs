namespace ConcordanceModel.Core
{
    /// <summary>
    /// Class used for representing an logical symbols.
    /// </summary>
    public class Symbol
    {
        #region Private Fields

        /// <summary>
        /// The _ value
        /// </summary>
        private readonly string _Value;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public Symbol(string c)
        {
            _Value = c;
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length
        {
            get { return _Value.Length; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value
        {
            get { return _Value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is upper case.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is upper case; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpperCase
        {
            get { return _Value.Length == 1 && char.IsLetter(_Value[0]) && char.IsUpper(_Value[0]); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is lower case.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is lower case; otherwise, <c>false</c>.
        /// </value>
        public bool IsLowerCase
        {
            get { return _Value.Length == 1 && char.IsLetter(_Value[0]) && char.IsLower(_Value[0]); }
        }
        
        #endregion
    }
}
