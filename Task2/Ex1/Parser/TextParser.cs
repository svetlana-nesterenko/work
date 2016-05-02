namespace Parser
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using TextModel.Core;

    #endregion

    /// <summary>
    /// Class used for representing the structure of the text parsing.
    /// </summary>
    public class TextParser
    {
        #region Private Fields

        /// <summary>
        /// The _ buffer size
        /// </summary>
        private int _BufferSize = 102400;
       
        /// <summary>
        /// The _ punctuation with multiple symbols
        /// </summary>
        private static readonly string[] _PunctuationWithMultipleSymbols = {"...", "!?"};

        /// <summary>
        /// The _ should use last paragraph
        /// </summary>
        private bool _ShouldUseLastParagraph = false;
        
        /// <summary>
        /// The _ last paragraph content
        /// </summary>
        private string _LastParagraphContent = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextParser"/> class.
        /// </summary>
        /// <param name="bufferSize">Size of the buffer.</param>
        public TextParser(int bufferSize)
        {
            _BufferSize = bufferSize;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses the specified stream.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Parses the sentence items.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public IEnumerable<ISentenceItem> ParseSentenceItems(string content)
        {
            Paragraph paragraph = new Paragraph();
            ParseSentence(content, paragraph);
            return paragraph.FirstOrDefault();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parses the sentence.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="paragraphObject">The paragraph object.</param>
        private void ParseSentence(string content, Paragraph paragraphObject)
        {
            if (!String.IsNullOrWhiteSpace(content))
            {
                Sentence sentenceObject = new Sentence();
                MatchCollection sentenceItemCollection = Regex.Matches(content, @"(\S+)", RegexOptions.Multiline);
                int currentIndex = 0;
                foreach (Match sentenceItemMatch in sentenceItemCollection)
                {
                    MatchCollection potentialWordCollection = Regex.Matches(sentenceItemMatch.Value, @"(\w*)(\W*)");
                    foreach (Match potentialWordItemMatch in potentialWordCollection)
                    {
                        
                        Group groupWord = potentialWordItemMatch.Groups[1];
                        string word = groupWord.Value;

                        Group groupOther = potentialWordItemMatch.Groups[2];
                        string punctuation = groupOther.Value;

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
                                int i = 0;
                                foreach (char c in punctuation)
                                {
                                    ISentenceItem item = new Punctuation(c.ToString());
                                    sentenceObject.Add(item);
                                    i++;
                                }
                            }
                        }
                    }

                    currentIndex++;

                    if (currentIndex < sentenceItemCollection.Count)
                    {
                        sentenceObject.Add(new WhiteSpace(" "));
                    }
                }

                paragraphObject.Add(sentenceObject);
            }
        }

        /// <summary>
        /// Parses the paragraph.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
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
                        ParseSentence(sentenceMatch.Value.Trim(), paragraphObject);
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

        /// <summary>
        /// Parses the several paragraphs.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="paragraphCollection">The paragraph collection.</param>
        /// <returns></returns>
        private int ParseSeveralParagraphs(Text text, MatchCollection paragraphCollection)
        {
            int paragraphsTextLength = 0;

            foreach (Match paragraphMatch in paragraphCollection)
            {
                paragraphsTextLength += paragraphMatch.Length;
                int sentenciesTextLength = ParseParagraph(text, paragraphMatch.Value);
                if (sentenciesTextLength < paragraphMatch.Value.Length)
                {
                    ParseSentence(paragraphMatch.Value.Substring(sentenciesTextLength), text.LastOrDefault());
                }
                _ShouldUseLastParagraph = false;
            }
            return paragraphsTextLength;
        }
        
        #endregion
    }
}
