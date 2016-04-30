using TextModel.Enum;
using TextModel.Helpers;

namespace TextModel.Core
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

        public bool IsLetter
        {
            get { return _Value.Length == 1 && char.IsLetter(_Value[0]); }
        }

        public bool IsNumber
        {
            get { return _Value.Length == 1 && char.IsDigit(_Value[0]); }
        }

        public bool IsPunctuation
        {
            get { return _Value.Length == 1 && char.IsPunctuation(_Value[0]); }
        }

        public bool IsUpperCase
        {
            get { return _Value.Length == 1 && char.IsLetter(_Value[0]) && char.IsUpper(_Value[0]); }
        }

        public bool IsLowerCase
        {
            get { return _Value.Length == 1 && char.IsLetter(_Value[0]) && char.IsLower(_Value[0]); }
        }

        public bool IsVowel
        {
            get
            {
                return _Value.Length == 1 && char.IsLetter(_Value[0]) && VowelConsonantHelper.SymbolDictionary.ContainsKey(char.ToLower(_Value[0])) &&
                       VowelConsonantHelper.SymbolDictionary[char.ToLower(_Value[0])] == LetterTypeEnum.Vowel;
            }
        }

        public bool IsConsonant
        {
            get
            {
                return _Value.Length == 1 && char.IsLetter(_Value[0]) && VowelConsonantHelper.SymbolDictionary.ContainsKey(char.ToLower(_Value[0])) &&
                       VowelConsonantHelper.SymbolDictionary[char.ToLower(_Value[0])] == LetterTypeEnum.Consonant;
            }
        }
    }
}
