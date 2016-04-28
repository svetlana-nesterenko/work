namespace Parser.Classes
{
    /// <summary>
    /// Class used for representing the Numeric symbols.
    /// </summary>
    /// <seealso cref="Parser.Classes.Symbol" />
    public class NumericSymbol: Symbol
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericSymbol"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public NumericSymbol(char c) : base(c)
        {
        }
    }
}
