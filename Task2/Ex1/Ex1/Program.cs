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

            if (!File.Exists(path))
            {
                Console.WriteLine(String.Format("File {0} does not exist. It will be created and filled with predefined data.", path));
                return;
            }
            else
            {
                Console.WriteLine(String.Format("File {0} exists and will be used for initial data.", path));
                string content = File.ReadAllText("test.txt");
                TextParser parser = new TextParser();
                text = parser.Parse(content);
                //using (TextParser parser = new TextParser())
                //{
                //    text = parser.Parse(content);
                //}
            }
            
            
            //string content = File.ReadAllText("test.txt");
            //TextParser parser = new TextParser();
            //Text text = parser.Parse(content);

            //var a = text.ToArray().Select(p => p.Items).Select(p => p.ToArray()).SelectMany(p => p.ToArray()).OrderBy(p => { return p.ToArray().Count(f => f is Word); });
            //foreach (var sen in a)
            //{
            //    Console.WriteLine(sen.CreateSentance());
            //}

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

            //Console.WriteLine(text.GetText());
            //Console.WriteLine(text.FormatText());
            //Console.WriteLine(text.DeleteWordsByLenght(6, LetterTypes.consonant));
            Console.WriteLine(text.Replace(6, "123456"));

            Console.ReadKey();
        }
    }
}
