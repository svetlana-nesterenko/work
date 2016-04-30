namespace Ex1
{
    #region Usings

    using System;
    using System.Configuration;
    using System.IO;
    using Parser;
    using TextModel.Core;

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["Path"];

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    TextParser parser = new TextParser();
                    Text text = parser.Parse(fs);
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
        }
    }
}
