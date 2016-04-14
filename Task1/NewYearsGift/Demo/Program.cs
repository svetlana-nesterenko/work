﻿namespace Demo
{
    #region Usings
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using NewYearsGift.Classes;
    using NewYearsGift.Interfaces;

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
           
            #region Creat demo gift

            Console.WriteLine(String.Format("Using path: {0}", ConfigurationManager.AppSettings["Path"]));
            string path = ConfigurationManager.AppSettings["Path"];
            if (String.IsNullOrEmpty(path))
            {
                Console.WriteLine("Incorrect path!");
                return;
            }

            XmlSerializer xml = new XmlSerializer(typeof (Gift),
                new Type[] {typeof (Toy), typeof (Box), typeof (Fruit), typeof (Candy)});
            Gift gift = null;

            if (!File.Exists(path))
            {
                Console.WriteLine(
                    String.Format("File {0} does not exist. It will be created and filled with predefined data.", path));
                gift = new Gift();
                GiftBilder builder = new GiftBilder(gift);
                builder.Build();

                using (TextWriter writer = new StreamWriter(path))
                {
                    xml.Serialize(writer, gift);
                    writer.Close();
                }
            }
            else
            {
                Console.WriteLine(String.Format("File {0} exists and will be used for initial data.", path));
                using (TextReader reader = new StreamReader(path))
                {
                    gift = (Gift) xml.Deserialize(reader);
                }
            }

            Console.WriteLine(String.Format("Gift name: {0}", gift.Name));
            Console.WriteLine(String.Format("Gift article: {0}", gift.ProductArticle));
            Console.WriteLine(String.Format("Gift consists from {0} items.", gift.Items.Count));
            Console.WriteLine(String.Format("Gift weight: {0}", gift.Weight));
            Console.WriteLine("==============================================");

            #endregion

            #region Sort

            //Sort variant 1 linq
            var SortByNameList = gift.Items.OrderBy(i => i.Name).ToList();
            PrintSortedList("name", SortByNameList);

            

            //var SortByWeightList = gift.Items.OrderBy(i => i.Weight).ToList();
            //PrintSortedList("weight", SortByWeightList);

            //Sort variant 2 delegate
            //gift.Items.Sort(new MySortByName());
            //PrintSortedList("name", gift.Items);

            //Sort variant 3 delegate
            //gift.Items.Sort((a, b) => String.Compare(a.Name, b.Name, StringComparison.Ordinal));
            //gift.Items.Sort((a, b) => a.Weight.CompareTo(b.Weight));

            #endregion

            #region Find by range of sugar content

            double x = 30;
            double y = 150;

            //Filter using linq
            List<Item> candyList = gift.Items.Where(i => i is ICandy && ((ICandy)i).SugarContent >= x && ((ICandy)i).SugarContent <= y).ToList();

            Console.WriteLine("Конфеты, соответствующие диапазону содержания сахара  {0}-{1}:", x, y);
            foreach (Item item in candyList)
            {
                Console.WriteLine("{0} {1} с содержанием сахара - {2}", ((ICandy)item).Category, item.Name, ((ICandy)item).SugarContent);
            }

            #endregion

            Console.ReadKey();
        }

        public static void PrintSortedList(string method, IEnumerable<IItem> items )
        {
            Console.WriteLine("Sort list by {0}:", method);
            foreach (Item item in items)
            {
                Console.WriteLine("{0} weight={1}", item.Name, item.Weight);
            }
            Console.WriteLine("==============================================");

        }

        public class MySortByName : IComparer<Item>
        {
            public int Compare(Item x, Item y)
            {
                Item item1 = (Item)x;
                Item item2 = (Item)y;
                return String.Compare(item1.Name, item2.Name, StringComparison.Ordinal);
            }
        }
    }
}
