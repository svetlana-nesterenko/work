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

        public override string ToString()
        {
            return _Symbol.Value.ToString();
        }

        public int GetLength()
        {
            return _Symbol.Length;
        }
    }
}
