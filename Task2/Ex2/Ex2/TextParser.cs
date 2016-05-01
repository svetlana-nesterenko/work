namespace Ex2
{
    #region Usings

    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using ConcordanceModel.Core;    

    #endregion

    /// <summary>
    /// Class used for representing the structure of the text parsing.
    /// </summary>
    public class TextParser
    {
        private int _PageSize = 10;
        private int _BufferSize = 102400;
        private string _LastStringContent = null;

        public TextParser(int pageSize)
        {
            _PageSize = pageSize;
        }

        public TextParser(int pageSize, int bufferSize)
        {
            _PageSize = pageSize;
            _BufferSize = bufferSize;
        }

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

                    if (_LastStringContent != null)
                    {
                        currentContent = _LastStringContent + currentContent;
                        _LastStringContent = null;
                    }

                    MatchCollection stringItemCollection = Regex.Matches(currentContent, @"(.*?(\n))", RegexOptions.Multiline);
                    if (stringItemCollection.Count > 0)
                    {
                        int stringsTextLength = ParseSeveralStringItems(text, stringItemCollection);
                        if (currentContent.Length > stringsTextLength)
                        {
                            _LastStringContent = currentContent.Substring(stringsTextLength);
                        }
                        else
                        {
                            _LastStringContent = null;
                        }
                    }
                    else
                    {
                        _LastStringContent = currentContent;
                    }
                }
            } while (bytesRead > 0);

            if (_LastStringContent != null)
            {
                ParseStringItem(text, _LastStringContent);
            }

            return text;
        }

        private void ParseStringItem(Text text, string content)
        {
            Page page = GetPage(text);;

            if (!String.IsNullOrWhiteSpace(content))
            {
                StringItem stringItemObject = new StringItem();

                MatchCollection sentenceItemCollection = Regex.Matches(content, @"(\S+)", RegexOptions.Multiline);
                int currentIndex = 0;
                foreach (Match sentenceItemMatch in sentenceItemCollection)
                {
                    MatchCollection potentialWordCollection = Regex.Matches(sentenceItemMatch.Value, @"([a-zA-Z]+)");
                    foreach (Match potentialWordItemMatch in potentialWordCollection)
                    {
                        Group groupWord = potentialWordItemMatch.Groups[1];
                        string word = groupWord.Value;

                        if (!String.IsNullOrEmpty(word))
                        {
                            Word item = new Word(word);
                            stringItemObject.Add(item);
                        }
                    }
                }
                page.Add(stringItemObject);
            }
        }

        private int ParseSeveralStringItems(Text text, MatchCollection stringItemCollection)
        {
            int stringTextLength = 0;

            foreach (Match stringItemMatch in stringItemCollection)
            {
                stringTextLength += stringItemMatch.Length;
                ParseStringItem(text, stringItemMatch.Value);
            }
            return stringTextLength;
        }

        private Page GetPage(Text text)
        {
            Page page;
            if (text.LastOrDefault() != null && text.LastOrDefault().Count < _PageSize)
            {
                page = text.LastOrDefault();
            }
            else
            {
                page = new Page(text.Count + 1);
                text.Add(page);
            }
            return page;
        }
    }
}
