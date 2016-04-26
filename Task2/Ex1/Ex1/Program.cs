using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Parser;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = File.ReadAllText("test.txt");
            TextParser parser = new TextParser();
            parser.Parse(content);
        }
    }
}
