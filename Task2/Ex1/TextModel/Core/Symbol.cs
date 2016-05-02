namespace TextModel.Core
{
    #region Usings

    using TextModel.Enum;
    using TextModel.Helpers;

    #endregion

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
        /// Gets a value indicating whether this instance is letter.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is letter; otherwise, <c>false</c>.
        /// </value>
        public bool IsLetter
        {
            get { return _Value.Length == 1 && char.IsLetter(_Value[0]); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is number.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is number; otherwise, <c>false</c>.
        /// </value>
        public bool IsNumber
        {
            get { return _Value.Length == 1 && char.IsDigit(_Value[0]); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is punctuation.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is punctuation; otherwise, <c>false</c>.
        /// </value>
        public bool IsPunctuation
        {
            get { return _Value.Length == 1 && char.IsPunctuation(_Value[0]); }
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

        /// <summary>
        /// Gets a value indicating whether this instance is vowel.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vowel; otherwise, <c>false</c>.
        /// </value>
        public bool IsVowel
        {
            get
            {
                return _Value.Length == 1 && char.IsLetter(_Value[0]) && VowelConsonantHelper.SymbolDictionary.ContainsKey(char.ToLower(_Value[0])) &&
                       VowelConsonantHelper.SymbolDictionary[char.ToLower(_Value[0])] == LetterTypeEnum.Vowel;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is consonant.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is consonant; otherwise, <c>false</c>.
        /// </value>
        public bool IsConsonant
        {
            get
            {
                return _Value.Length == 1 && char.IsLetter(_Value[0]) && VowelConsonantHelper.SymbolDictionary.ContainsKey(char.ToLower(_Value[0])) &&
                       VowelConsonantHelper.SymbolDictionary[char.ToLower(_Value[0])] == LetterTypeEnum.Consonant;
            }
        }
        
        #endregion
    }
}
