using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser
{
    public class TextParser
    {
        public void Parse(string content)
        {
            MatchCollection paragraphCollection = Regex.Matches(content, @"(.*?(\n|$))", RegexOptions.Multiline);
            foreach (Match paragraph in paragraphCollection)
            {
                if (!String.IsNullOrEmpty(paragraph.Value.Trim()))
                {
                    MatchCollection sentenciesCollection = Regex.Matches(paragraph.Value, @"(.*?(\.+|\n|!\?|\!|\?|$))", RegexOptions.Multiline);
                    foreach (Match sentence in sentenciesCollection)
                    {
                        if (!String.IsNullOrWhiteSpace(sentence.Value.Trim()))
                        {
                            char endSymbol = sentence.Value[sentence.Value.Trim().Length - 1];
                            switch (endSymbol)
                            {
                                case '.':
                                    break;
                                case '!':
                                    break;
                                case '?':
                                    break;
                                default:
                                    break;
                            }

                            MatchCollection sentenceItemCollection = Regex.Matches(sentence.Value.Trim(), @"(\S+)", RegexOptions.Multiline);
                            foreach (Match sentenceItem in sentenceItemCollection)
                            {
                                MatchCollection potentialWordCollection = Regex.Matches(sentenceItem.Value, @"(\w*)(\W*)");
                                foreach (Match potentialWordItem in potentialWordCollection)
                                {
                                    Group groupWord = potentialWordItem.Groups[1];
                                    string word = groupWord.Value.Trim();
                                    Group groupOther = potentialWordItem.Groups[2];
                                    string punctuation = groupOther.Value.Trim();

                                    if (!String.IsNullOrEmpty(word))
                                    {
                                        //Console.Write(" " + word);
                                    }

                                    if (!String.IsNullOrEmpty(punctuation))
                                    {
                                        //Console.Write("" + punctuation);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
