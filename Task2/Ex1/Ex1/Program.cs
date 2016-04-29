namespace Ex1
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using Parser;
    using Parser.Classes;
    using Parser.Interfaces;

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(String.Format("Using path: {0}", ConfigurationManager.AppSettings["Path"]));
            string path = ConfigurationManager.AppSettings["Path"];
            if (String.IsNullOrEmpty(path))
            {
                Console.WriteLine("Incorrect path!");
                return;
            }

            Text text = null;

            //if (!File.Exists(path))
            //{
            //    Console.WriteLine(String.Format("File {0} does not exist. It will be created and filled with predefined data.", path));
            //    return;
            //}
            //else
            //{
            //    Console.WriteLine(String.Format("File {0} exists and will be used for initial data.", path));
            //    string content = File.ReadAllText(path);
            //    TextParser parser = new TextParser();
            //    text = parser.Parse(content);
            //}

            try
            {
                Console.WriteLine(String.Format("File {0} exists and will be used for initial data.", path));
                string content = File.ReadAllText(path);
                TextParser parser = new TextParser();
                text = parser.Parse(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("File {0} does not exist.", path));
                return;
            }

            //display all sentences of the text in ascending order of the number of words in each of them.
            Console.WriteLine("====================================================");
            Console.WriteLine("Sorted sentences by number of words:");
            IEnumerable<ISentence> sortedSentences = text.SortSentencesByWordCount();
            foreach (ISentence sen in sortedSentences)
            {
                Console.WriteLine(sen.CreateSentence());
            }

            //in all interrogative sentences of the text find the words of a given length.
            int lenght = 3;
            IEnumerable<string> findedWords = text.FindWordsInInterrogativeSentences(lenght);
            Console.WriteLine("====================================================");
            Console.WriteLine("Word with lenght = {0} in interrogative sentences:", lenght);
            foreach (string word in findedWords)
            {
                Console.WriteLine(word);
            }

            //remove from the text all words of a given length, beginning with a consonant letter.
            int length2 = 6;
            Console.WriteLine("====================================================");
            Console.WriteLine("Text without words wich have lenght = {0}:", length2);
            Console.WriteLine(text.DeleteWordsByLenght(length2, LetterTypes.consonant));

            //replace words
            int length3 = 7;
            int paragraphIndex = 0;
            int sentenceIndex = 3;
            Console.WriteLine("====================================================");
            Console.WriteLine("Text with replacing words:");
            Console.WriteLine(text.Replace(paragraphIndex + 100, sentenceIndex, length3, "123456789"));

            text.ExportToFile("text.txt");

            Console.ReadKey();
        }
    }
}
