namespace TextModel.Core
{
    public class Punctuation :  IPunctuation
    {
        private Symbol _Symbol;

        public Punctuation(string p)
        {
            _Symbol = new Symbol(p);
        }

        public string Value
        {
            get
            {
                return _Symbol.Value.ToString();
            }
        }

        public int Position { get; private set; }
    }
}
