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
            if (ConfigurationManager.AppSettings["InputFile"] == null)
            {
                Console.WriteLine("Please specify \"InputFile\" path in app.config");
                return;
            }

            if (ConfigurationManager.AppSettings["OutputFile"] == null)
            {
                Console.WriteLine("Please specify \"OutputFile\" path in app.config");
                return;
            }

            string inputFile = ConfigurationManager.AppSettings["InputFile"];
            string outputFile = ConfigurationManager.AppSettings["OutputFile"];

            int bufferSize;
            if (ConfigurationManager.AppSettings["BufferSize"] == null || !int.TryParse(ConfigurationManager.AppSettings["BufferSize"].ToString(), out bufferSize))
            {
                bufferSize = 102400;
                Console.WriteLine("Will be used default size of buffer (100Kb).");
            }

            TextParser parser = new TextParser(bufferSize);
            Text text = null;
            try
            {
                using (FileStream fs = new FileStream(inputFile, FileMode.Open))
                {
                    text = parser.Parse(fs);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: File {0} not found", inputFile);
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Error: Path {0} too long", inputFile);
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

            //display all sentences of the text in ascending order of the number of words in each of them
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Get sentences ordered by word count:");
            Console.WriteLine();
            var sentencies = text.GetSentencesOrderedByWordCount();
            foreach (var sentence in sentencies)
            {
                Console.WriteLine(sentence);
            }

            //in all interrogative sentences of the text find the words of a given length
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            int lenght = 4;
            Console.WriteLine("Word with lenght = {0} in interrogative sentences:", lenght);
            Console.WriteLine();
            var words = text.FindWordsInInterrogativeSentences(lenght);
            foreach (string s in words)
            {
                Console.WriteLine(s);
            }

            //remove from the text all words of a given length, beginning whith a consonant letter
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            int lenght2 = 6;
            try
            {
                text.DeleteWordsByLenght(lenght2);
                Console.WriteLine(
                    "The words which beginning at a consonant letter and have length = {0} was removed from the text.",
                    lenght2);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //replace words in a given sentence
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            string textReplace = "hello world";
            IEnumerable<ISentenceItem> newItems = parser.ParseSentenceItems(textReplace);

            try
            {
                text.Replace(0, 0, 4, newItems);
                Console.WriteLine("Replaced OK.");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //export to file
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            
            try
            {
                text.ExportToFile(outputFile, bufferSize);
                Console.WriteLine("Exported to file \"{0}\"", outputFile);
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
