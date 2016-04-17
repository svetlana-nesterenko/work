namespace Demo
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            Console.WriteLine("==============================================");
            Console.WriteLine("Gift summary:");
            Console.WriteLine("");
            Console.WriteLine(String.Format("Name: {0}", gift.Name));
            Console.WriteLine(String.Format("Article: {0}", gift.ProductArticle));
            Console.WriteLine(String.Format("Contains: {0} items", gift.Count));
            Console.WriteLine(String.Format("Weight: {0}", gift.Weight));

            //Sorting candies in the gift by weight
            Console.WriteLine("");
            Console.WriteLine("==============================================");
            Console.WriteLine("Candies ordered by weight:");
            Console.WriteLine("");
            IEnumerable<IItem> candiesByWeight = gift.Where(i => i is ICandy).OrderBy(c => c.Weight);
            foreach (IItem item in candiesByWeight)
            {
                Console.WriteLine("Name \"{0}\" weight: {1}", item.FullName, item.Weight);
            }

            //Sorting items in the gift by name
            Console.WriteLine("");
            Console.WriteLine("==============================================");
            Console.WriteLine("All items sorted by name:");
            Console.WriteLine("");
            IEnumerable<IItem> sortedItems = gift.SortItemsByName();
            foreach (IItem item in sortedItems)
            {
                Console.WriteLine("{0}", item.Name);
            }

            //Find by range of sugar content
            Console.WriteLine("");
            Console.WriteLine("==============================================");
            Console.WriteLine("Items with the sugar 50-150:");
            Console.WriteLine("");
            IEnumerable<IItem> itemsWithSugar = gift.FindBySugarContent(50, 150);
            foreach (IItem item in itemsWithSugar)
            {
                Console.WriteLine(item.GetInfo());
            }

            Console.ReadKey();
        }
    }
}
