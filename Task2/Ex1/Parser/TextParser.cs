using System.IO;
using System.Linq;
using System.Text;
using TextModel.Core;

namespace Parser
{
    #region Usings

    using System;
    using System.Text.RegularExpressions;

    #endregion

    /// <summary>
    /// Class used for representing the structure of the text parsing.
    /// </summary>
    public class TextParser
    {
        private const int _BufferSize = 20;
        private static readonly string[] _PunctuationWithMultipleSymbols = {"...", "!?"};

        private bool _ShouldUseLastParagraph = false;
        private string _LastParagraphContent = null;

        public Text Parse(Stream s)
        {
            Text text = new Text();

            int bytesRead = 0;
            byte[] buffer = new byte[_BufferSize];
            do
            {
                bytesRead = s.Read(buffer, 0, _BufferSize);
                if (bytesRead > 0)
                {
                    string currentContent = Encoding.Default.GetString(buffer, 0, bytesRead);

                    if (_LastParagraphContent != null)
                    {
                        currentContent = _LastParagraphContent + currentContent;
                        _LastParagraphContent = null;
                    }

                    MatchCollection paragraphCollection = Regex.Matches(currentContent, @"(.*?(\n))",
                        RegexOptions.Multiline);
                    if (paragraphCollection.Count > 0)
                    {
                        int paragraphsTextLength = ParseSeveralParagraphs(text, paragraphCollection);
                        if (currentContent.Length > paragraphsTextLength)
                        {
                            _LastParagraphContent = currentContent.Substring(paragraphsTextLength);
                            //_ShouldUseLastParagraph = true;
                        }
                        else
                        {
                            _LastParagraphContent = null;
                            _ShouldUseLastParagraph = false;
                        }
                    }
                    else
                    {
                        ParseParagraph(text, currentContent);
                    }
                }
            } while (bytesRead > 0);

            if (_LastParagraphContent != null)
            {
                _ShouldUseLastParagraph = false;
                ParseParagraph(text, _LastParagraphContent);
            }

            return text;
        }

        private void ParseSentence(Text text, string content, Paragraph paragraphObject)
        {
            if (!String.IsNullOrWhiteSpace(content))
            {
                Sentence sentenceObject = new Sentence();
                MatchCollection sentenceItemCollection = Regex.Matches(content, @"(\S+)", RegexOptions.Multiline);
                foreach (Match sentenceItemMatch in sentenceItemCollection)
                {
                    MatchCollection potentialWordCollection = Regex.Matches(sentenceItemMatch.Value, @"(\w*)(\W*)");
                    foreach (Match potentialWordItemMatch in potentialWordCollection)
                    {
                        Group groupWord = potentialWordItemMatch.Groups[1];
                        string word = groupWord.Value.Trim();

                        Group groupOther = potentialWordItemMatch.Groups[2];
                        string punctuation = groupOther.Value.Trim();

                        if (!String.IsNullOrEmpty(word))
                        {
                            ISentenceItem item = new Word(word);
                            sentenceObject.Add(item);
                        }

                        if (!String.IsNullOrEmpty(punctuation))
                        {
                            if (_PunctuationWithMultipleSymbols.Contains(punctuation))
                            {
                                ISentenceItem item = new Punctuation(punctuation);
                                sentenceObject.Add(item);
                            }
                            else
                            {
                                foreach (char c in punctuation)
                                {
                                    ISentenceItem item = new Punctuation(c.ToString());
                                    sentenceObject.Add(item);
                                }
                            }
                        }
                    }
                }

                paragraphObject.Add(sentenceObject);
            }
        }

        private int ParseParagraph(Text text, string content)
        {
            int sentenciesTextLength = 0;
            if (!String.IsNullOrEmpty(content))
            {
                Paragraph paragraphObject = _ShouldUseLastParagraph ? (text.LastOrDefault() ?? new Paragraph()) : new Paragraph();

                MatchCollection sentenciesCollection = Regex.Matches(content, @"(.*?(\.+|\?+|\!+))", RegexOptions.Multiline);
                foreach (Match sentenceMatch in sentenciesCollection)
                {
                    sentenciesTextLength += sentenceMatch.Length;

                    if (!String.IsNullOrWhiteSpace(sentenceMatch.Value.Trim()))
                    {
                        ParseSentence(text, sentenceMatch.Value.Trim(), paragraphObject);
                    }
                }

                if (!_ShouldUseLastParagraph)
                {
                    text.Add(paragraphObject);
                }
            }

            if (sentenciesTextLength < content.Length)
            {
                _ShouldUseLastParagraph = true;
                _LastParagraphContent = content.Substring(sentenciesTextLength);
            }
            return sentenciesTextLength;
        }

        private int ParseSeveralParagraphs(Text text, MatchCollection paragraphCollection)
        {
            int paragraphsTextLength = 0;

            foreach (Match paragraphMatch in paragraphCollection)
            {
                paragraphsTextLength += paragraphMatch.Length;
                int sentenciesTextLength = ParseParagraph(text, paragraphMatch.Value);
                if (sentenciesTextLength < paragraphMatch.Value.Length)
                {
                    ParseSentence(text, paragraphMatch.Value.Substring(sentenciesTextLength), text.LastOrDefault());
                }
                _ShouldUseLastParagraph = false;
            }
            return paragraphsTextLength;
        }
    }
}
