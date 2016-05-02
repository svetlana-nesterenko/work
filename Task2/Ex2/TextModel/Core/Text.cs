namespace ConcordanceModel.Core
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using TextModel.Exceptions;

    #endregion

    /// <summary>
    /// This class represents the text which consist from pages.
    /// </summary>
    /// <seealso cref="Page" />
    public class Text : BaseEnumerable<Page>
    {
        #region Public Methods

        /// <summary>
        /// Creates the concordance.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ConcordanceItem> CreateConcordance()
        {
            Dictionary<string, ConcordanceItem> ht = new Dictionary<string, ConcordanceItem>();

            foreach (Page p in this)
            {
                foreach (string key in p.SelectMany(stringItem => stringItem.Select(word => word.ToString().ToLower())))
                {
                    if (ht.ContainsKey(key))
                    {
                        ht[key].AddOccurrence(p.PageIndex);
                    }
                    else
                    {
                        ConcordanceItem item = new ConcordanceItem();
                        item.AddOccurrence(p.PageIndex);
                        ht.Add(key, item);
                    }
                }
            }

            return ht;
        }

        /// <summary>
        /// Exports the concordance to file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <exception cref="UnexpectedException">
        /// I/O Exception
        /// or
        /// Unexpected error.
        /// </exception>
        public void ExportConcordanceToFile(string filename, int bufferSize)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                Dictionary<string, ConcordanceItem> concordance = CreateConcordance();

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    IEnumerable<string> keys = concordance.Keys;
                    char title = (char)0;
                    foreach (string key in keys.OrderBy(k => k))
                    {
                        ConcordanceItem item = concordance[key];

                        if (title != key[0])
                        {
                            sb.AppendLine(key[0].ToString().ToUpper());
                            title = key[0];
                        }

                        sb.Append(key);
                        for (int i = 0; i < 20 - (key.Length < 20 ? key.Length : 17); i++)
                        {
                            sb.Append(".");
                        }
                        sb.Append(item.Count);
                        sb.Append(": ");
                        foreach (int page in item.PageIndexes)
                        {
                            sb.Append(page);
                            sb.Append(" ");
                        }
                        sb.AppendLine();

                        if (sb.Length > bufferSize)
                        {
                            byte[] buffer = Encoding.Default.GetBytes(sb.ToString());
                            fs.Write(buffer, 0, bufferSize);
                            sb.Clear();
                            sb.Append(Encoding.Default.GetString(buffer, bufferSize, buffer.Length - bufferSize));
                        }
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
        
        #endregion
    }
}
