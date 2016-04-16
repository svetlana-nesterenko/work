namespace Demo
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
            //Creating demo gift
            Gift gift = new Gift();
            GiftBilder builder = new GiftBilder(gift);
            builder.Build();

            gift.RecalculateWeight();
            Console.WriteLine(String.Format("Gift name: {0}", gift.Name));
            Console.WriteLine(String.Format("Gift article: {0}", gift.ProductArticle));
            Console.WriteLine(String.Format("Gift consists from {0} items.", gift.Count));
            Console.WriteLine(String.Format("Gift weight: {0}", gift.Weight));
            Console.WriteLine("==============================================");

            //Sorting items in the gift
            IEnumerable<IItem> sortedItems = gift.SortItemsByName();
            Console.WriteLine("Items sorted by name:");
            foreach (IItem item in sortedItems)
            {
                Console.WriteLine("{0}", item.Name);
            }


            #region Find by range of sugar content

            IEnumerable<IItem> itemsWithSugar = gift.FindBySugarContent(50, 150);
            Console.WriteLine("Items with the sugar 50-150:");
            foreach (Item item in itemsWithSugar)
            {
                Console.WriteLine("Name: {0}   Sugar: {1}", item.Name, ((IHasSugar)item).SugarContent);
            }

            #endregion

            Console.ReadKey();
        }
    }
}
