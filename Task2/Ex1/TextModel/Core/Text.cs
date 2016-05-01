using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextModel.Enum;
using TextModel.Exceptions;

namespace TextModel.Core
{
    /// <summary>
    /// This class represents the text which consist from paragraphs.
    /// </summary>
    /// <seealso cref="Paragraph" />
    public class Text : BaseEnumerable<Paragraph>
    {
        public IEnumerable<Sentence> GetSentencesOrderedByWordCount()
        {
            return _Items.SelectMany(s => s).OrderBy(i => { return i.ToArray().Count(w => w is IWord); });
        }

        /// <summary>
        /// Finds the words in interrogative sentences by a given length.
        /// </summary>
        /// <param name="lenght">The lenght.</param>
        /// <returns></returns>
        public IEnumerable<string> FindWordsInInterrogativeSentences(int lenght)
        {
            return _Items.SelectMany(s => s.ToArray()).Where(s => s.GetSentenceType() == SentenceTypeEnum.Interrogative).SelectMany(i => i.ToArray()).
                Where(i => i is IWord).Cast<IWord>().Where(w => w.GetLength() == lenght).Select(w => w.ToString()).Distinct().ToArray();
        }

        /// <summary>
        /// Deletes the words by a given lenght and wich begins on consonant letter.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public void DeleteWordsByLenght(int length)
        {
            foreach (var paragraph in _Items)
            {
                foreach (var sentence in paragraph)
                {
                    foreach (var item in sentence)
                    {
                        if (item is IWord)
                        {
                            var firstSymbol = ((IWord) item)[0];
                            if (firstSymbol != null && item.ToString().Length == length && firstSymbol.IsConsonant)
                            {
                                sentence.Remove(item);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replaces words of the specified length by substring.
        /// </summary>
        /// <param name="paragraphIndex">Index of the paragraph.</param>
        /// <param name="sentenceIndex">Index of the sentence.</param>
        /// <param name="length">The length.</param>
        /// <param name="replacingItems">The replacing items.</param>
        /// <exception cref="System.IndexOutOfRangeException">Index is out of range.</exception>
        public void Replace(int paragraphIndex, int sentenceIndex, int length, IEnumerable<ISentenceItem> replacingItems)
        {
            Sentence searchSentence = ((Paragraph) _Items[paragraphIndex]).ElementAt(sentenceIndex);
            for (int i = 0; i < searchSentence.Count; i++)
            {
                var item = searchSentence.ElementAt(i);
                if (item is IWord && item.ToString().Length == length)
                {
                    searchSentence.Remove(item);
                    for (int j = 0; j < replacingItems.Count(); j++)
                    {
                        var newItem = replacingItems.ElementAt(j);
                        searchSentence.Insert(newItem, i + j);
                    }
                }
            }
        }

        /// <summary>
        /// Exports to file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <exception cref="UnexpectedException">I/O Exception
        /// or
        /// Unexpected error.</exception>
        public void ExportToFile(string filename, int bufferSize)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    foreach (var paragraph in _Items)
                    {
                        foreach (var sentence in paragraph)
                        {
                            sb.Append(sentence.ToString());

                            if (sb.Length > bufferSize)
                            {
                                byte[] buffer = Encoding.Default.GetBytes(sb.ToString());
                                fs.Write(buffer, 0, bufferSize);
                                sb.Clear();
                                sb.Append(Encoding.Default.GetString(buffer, bufferSize, buffer.Length - bufferSize));
                            }
                        }

                        sb.Append("\r\n");
                    }

                    if (sb.Length > 0)
                    {
                        byte[] buffer = Encoding.Default.GetBytes(sb.ToString());
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (PathTooLongException ex)
            {
                throw new UnexpectedException(String.Format("Error: Path {0} too long", filename), ex);
            }
            catch (IOException ex)
            {
                throw new UnexpectedException("I/O Exception", ex);
            }
            catch (Exception ex)
            {
                throw new UnexpectedException("Unexpected error.", ex);
            }
        }
    }
}
