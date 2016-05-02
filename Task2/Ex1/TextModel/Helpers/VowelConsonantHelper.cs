namespace TextModel.Helpers
{
    #region Usings

    using System.Collections.Generic;
    using TextModel.Enum;

    #endregion

    /// <summary>
    /// Class used for defining  the letter type. 
    /// </summary>
    public static class VowelConsonantHelper
    {
        /// <summary>
        /// The symbol dictionary.
        /// </summary>
        public static Dictionary<char, LetterTypeEnum> SymbolDictionary = new Dictionary<char, LetterTypeEnum>
        {
            {'a', LetterTypeEnum.Vowel},
            {'b', LetterTypeEnum.Consonant},
            {'c', LetterTypeEnum.Consonant},
            {'d', LetterTypeEnum.Consonant},
            {'e', LetterTypeEnum.Vowel},
            {'f', LetterTypeEnum.Consonant},
            {'g', LetterTypeEnum.Consonant},
            {'h', LetterTypeEnum.Consonant},
            {'i', LetterTypeEnum.Vowel},
            {'j', LetterTypeEnum.Consonant},
            {'k', LetterTypeEnum.Consonant},
            {'l', LetterTypeEnum.Consonant},
            {'m', LetterTypeEnum.Consonant},
            {'n', LetterTypeEnum.Consonant},
            {'o', LetterTypeEnum.Vowel},
            {'p', LetterTypeEnum.Consonant},
            {'q', LetterTypeEnum.Consonant},
            {'r', LetterTypeEnum.Consonant},
            {'s', LetterTypeEnum.Consonant},
            {'t', LetterTypeEnum.Consonant},
            {'u', LetterTypeEnum.Vowel},
            {'v', LetterTypeEnum.Consonant},
            {'w', LetterTypeEnum.Consonant},
            {'x', LetterTypeEnum.Consonant},
            {'y', LetterTypeEnum.Vowel},
            {'z', LetterTypeEnum.Consonant}
        };
    }
}
