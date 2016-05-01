using System;
using System.Configuration;
using System.IO;
using ConcordanceModel.Core;
using TextModel.Exceptions;

namespace Ex2
{
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

            int pageSize;
            if (ConfigurationManager.AppSettings["PageSize"] == null || !int.TryParse(ConfigurationManager.AppSettings["PageSize"].ToString(), out pageSize))
            {
                pageSize = 10;
                Console.WriteLine("Will be used default page size (10 strings).");
            }

            int bufferSize;
            if (ConfigurationManager.AppSettings["BufferSize"] == null || !int.TryParse(ConfigurationManager.AppSettings["BufferSize"].ToString(), out bufferSize))
            {
                bufferSize = 102400;
                Console.WriteLine("Will be used default size of buffer (100Kb).");
            }

            TextParser parser = new TextParser(pageSize, bufferSize);
            Text text = null;
            try
            {
                using (FileStream fs = new FileStream(inputFile, FileMode.Open))
                {
                    text = parser.Parse(fs);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Error: File {0} not found", inputFile);
            }
            catch (PathTooLongException ex)
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
                Console.ReadKey();
                return;
            }


            try
            {
                text.ExportConcordanceToFile(outputFile, bufferSize);
                Console.WriteLine("Concordance was exported to file \"{0}\"", outputFile);
            }
            catch (UnexpectedException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            Console.ReadKey();
        }
    }
}
