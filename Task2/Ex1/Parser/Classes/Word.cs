namespace Parser.Classes
{
    #region Usings

    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Parser.Interfaces;

    #endregion

    /// <summary>
    /// Class represents the symbol or numeric part of sentence.
    /// </summary>
    /// <seealso cref="Parser.Interfaces.IWord" />
    public class Word: IWord
    {
        #region Private Fields

        /// <summary>
        /// The symbols.
        /// </summary>
        private Symbol[] symbols;

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
            get { return (symbols != null) ? symbols.Length : 0; }
        }

        /// <summary>
        /// Gets the chars.
        /// </summary>
        /// <value>
        /// The chars.
        /// </value>
        public string Chars
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var s in this.symbols)
                {
                    sb.Append(s.Char);
                }
                return sb.ToString();
            }
            set
            {
                List<Symbol> list = new List<Symbol>();
                foreach (char c in value)
                {
                    if (c >= 48 && c <= 57)
                    {
                        list.Add(new NumericSymbol(c));
                    }
                    else
                    {
                        list.Add(new LetterSymbol(c));
                    }
                }
                this.symbols = list.ToArray();
            }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Word"/> class.
        /// </summary>
        /// <param name="chars">The chars.</param>
        public Word(string chars)
        {
            if (chars != null)
            {
                List<Symbol> list = new List<Symbol>();
                foreach (char c in chars)
                {
                    if (c >= 48 && c <= 57)
                    {
                        list.Add(new NumericSymbol(c));
                    }
                    else
                    {
                        list.Add(new LetterSymbol(c));
                    }
                }
                this.symbols = list.ToArray();
            }
            else
            {
                this.symbols = null;
            }
        }

        #endregion
        
        #region Implementation of IEnumerable

        /// <summary>
        /// Gets the <see cref="Symbol"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Symbol"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Symbol this[int index]
        {
            get { return this.symbols[index]; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Symbol> GetEnumerator()
        {
            return symbols.AsEnumerable().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.symbols.GetEnumerator();
        }
        
        #endregion
    }
}
