﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Parser;
using Parser.Classes;
using Parser.Interfaces;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = File.ReadAllText("test.txt");
            TextParser parser = new TextParser();
            Text text = parser.Parse(content);

            var a = text.ToArray().Select(p => p.Items).Select(p => p.ToArray()).SelectMany(p => p.ToArray()).OrderBy(p => { return p.ToArray().Count(f => f is Word); });
            foreach (var sen in a)
            {
                Console.WriteLine(sen.CreateSentance());
            }

            Console.ReadKey();
        }
    }
}
