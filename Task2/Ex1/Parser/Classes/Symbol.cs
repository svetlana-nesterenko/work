namespace Parser.Classes
{
    /// <summary>
    /// Class used for representing any kind of symbols.
    /// </summary>
    public abstract class Symbol
    {
        /// <summary>
        /// The _char
        /// </summary>
        private char _char;
        
        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <value>
        /// The character.
        /// </value>
        public char Char
        {
            get { return _char; }
            private set { _char = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public Symbol(char c)
        {
            this._char = c;
        }
    }
}
