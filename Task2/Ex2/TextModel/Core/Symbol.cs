namespace ConcordanceModel.Core
{
    public class Symbol
    {
        private readonly string _Value;

        public Symbol(string c)
        {
            _Value = c;
        }

        public int Length
        {
            get { return _Value.Length; }
        }

        public string Value
        {
            get { return _Value; }
        }

        public bool IsUpperCase
        {
            get { return _Value.Length == 1 && char.IsLetter(_Value[0]) && char.IsUpper(_Value[0]); }
        }

        public bool IsLowerCase
        {
            get { return _Value.Length == 1 && char.IsLetter(_Value[0]) && char.IsLower(_Value[0]); }
        }
    }
}
