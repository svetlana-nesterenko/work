namespace Ex1
{
    #region Usings

    using System;
    using System.Configuration;
    using System.IO;
    using Parser;
    using TextModel.Core;
    using System.Collections.Generic;
    using TextModel.Exceptions;

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["Path"];

            TextParser parser = new TextParser();
            Text text = null;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    text = parser.Parse(fs);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Error: File {0} not found", path);
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine("Error: Path {0} too long", path);
            }
            catch (IOException ex)
            {
                Console.WriteLine("I/O Exception");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error.");
                Console.WriteLine(ex.Message);
            }

            if (text == null)
            {
                Console.WriteLine("Fatal error.");
                return;
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("TASK 1");
            Console.WriteLine();
            var sentencies = text.GetSentencesOrderedByWordCount();
            foreach (var sentence in sentencies)
            {
                Console.WriteLine(sentence);
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("TASK 2");
            Console.WriteLine();
            var words = text.FindWordsInInterrogativeSentences(6);
            foreach (string s in words)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("TASK 3");
            Console.WriteLine();
            string textReplace = "hello world";
            IEnumerable<ISentenceItem> bla1 = parser.ParseSentenceItems(textReplace);

            try
            {
                text.Replace(0, 0, 4, bla1);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("TASK 4");
            Console.WriteLine();

            try
            {
                text.ExportToFile("mytext.txt");
            }
            catch (UnexpectedException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            Console.ReadKey();
        }
    }
}
