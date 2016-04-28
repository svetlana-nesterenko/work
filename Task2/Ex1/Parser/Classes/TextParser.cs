namespace Parser
{
    #region Usings

    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Parser.Classes;
    using Parser.Interfaces;

    #endregion

    /// <summary>
    /// Class used for representing the structure of the text parsing.
    /// </summary>
    public class TextParser
    {
        public Text Parse(string content)
        {
            Text text = new Text();

            //Parse paragraphs
            MatchCollection paragraphCollection = Regex.Matches(content, @"(.*?(\n|$))", RegexOptions.Multiline);
            foreach (Match paragraph in paragraphCollection)
            {
                if (!String.IsNullOrEmpty(paragraph.Value.Trim()))
                {
                    Paragraph paragraphObject = new Paragraph();

                    //Parse sentencies
                    MatchCollection sentenciesCollection = Regex.Matches(paragraph.Value, @"(.*?(\.+|\?+|\!+|$|\n))", RegexOptions.Multiline);
                    foreach (Match sentence in sentenciesCollection)
                    {
                        if (!String.IsNullOrWhiteSpace(sentence.Value.Trim()))
                        {
                            Sentence sentenceObject;

                            char endSymbol = sentence.Value[sentence.Value.Trim().Length - 1];
                            switch (endSymbol)
                            {
                                case '.':
                                    sentenceObject = new DeclarativeSentence();
                                    break;
                                case '!':
                                    sentenceObject = new ImperativeSentence();
                                    break;
                                case '?':
                                    sentenceObject = new InterrogativeSentence();
                                    break;
                                default:
                                    sentenceObject = new OtherSentence();
                                    break;
                            }

                            //Parse sentence items (word, punctuation)
                            MatchCollection sentenceItemCollection = Regex.Matches(sentence.Value.Trim(), @"(\S+)", RegexOptions.Multiline);
                            foreach (Match sentenceItem in sentenceItemCollection)
                            {
                                //Parse word or punctuation
                                MatchCollection potentialWordCollection = Regex.Matches(sentenceItem.Value, @"(\w*)(\W*)");
                                foreach (Match potentialWordItem in potentialWordCollection)
                                {
                                    Group groupWord = potentialWordItem.Groups[1];
                                    string word = groupWord.Value.Trim();
                                    Group groupOther = potentialWordItem.Groups[2];
                                    string punctuation = groupOther.Value.Trim();
                                    
                                    if (!String.IsNullOrEmpty(word))
                                    {
                                        ISentenceItem item = new Word(word);
                                        sentenceObject.Add(item);
                                        if (sentenceObject is OtherSentence)
                                        {
                                            ((OtherSentence)sentenceObject).SetLastSign(null);
                                        }
                                    }

                                    if (!String.IsNullOrEmpty(punctuation))
                                    {
                                        foreach (char c in punctuation)
                                        {
                                            ISentenceItem item = new PunctuationSign(c.ToString());
                                            sentenceObject.Add(item);
                                            if (sentenceObject is OtherSentence)
                                            {
                                                ((OtherSentence)sentenceObject).SetLastSign((PunctuationSign)item);
                                            }
                                        }
                                    }
                                }
                            }

                            //if (sentenceObject.GetLastSign() != null && sentenceObject.Items[sentenceObject.Items.Count - 1].Chars == sentenceObject.GetLastSign().Chars)
                            //{
                            //    sentenceObject.Remove(sentenceObject.Items[sentenceObject.Items.Count-1]);
                            //}

                            //var lastWord = sentenceObject.Items.LastOrDefault(i => i is IWord);
                            //if (lastWord != null)
                            //{
                            //    int indexLastWord = sentenceObject.Items.IndexOf(lastWord);
                            //    for (int i = indexLastWord + 1; i < sentenceObject.Items.Count; i++)
                            //    {
                            //        //sentenceObject.Remove(sentenceObject.Items[i]);
                            //    }
                            //}

                            paragraphObject.Add(sentenceObject);
                        }
                    }
                    text.Add(paragraphObject);
                }
            }

            return text;
        }
    }
}
