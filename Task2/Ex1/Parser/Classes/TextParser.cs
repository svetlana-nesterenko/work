using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Parser.Classes;
using Parser.Interfaces;

namespace Parser
{
    public class TextParser
    {
        public Text Parse(string content)
        {
            Text text = new Text();

            MatchCollection paragraphCollection = Regex.Matches(content, @"(.*?(\n|$))", RegexOptions.Multiline);
            foreach (Match paragraph in paragraphCollection)
            {
                if (!String.IsNullOrEmpty(paragraph.Value.Trim()))
                {
                    Paragraph paragraphObject = new Paragraph();

                    MatchCollection sentenciesCollection = Regex.Matches(paragraph.Value, @"(.*?(\.+|\n|!\?|\!|\?|$))", RegexOptions.Multiline);
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
                                        ISentenceItem item = new Word(word);
                                        sentenceObject.Add(item);
                                    }

                                    if (!String.IsNullOrEmpty(punctuation))
                                    {
                                        ISentenceItem item = new PunctuationSign(punctuation);
                                        sentenceObject.Add(item);
                                    }
                                }
                            }

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
