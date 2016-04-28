namespace Parser.Classes
{
    /// <summary>
    /// Class used for representing the sign symbols.
    /// </summary>
    /// <seealso cref="Parser.Classes.Symbol" />
    public abstract class SignSymbol: Symbol
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignSymbol"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public SignSymbol(char c)
            : base(c)
        {
        }
    }
}
