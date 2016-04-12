namespace Demo
{
    #region Usings

    using System;
    using System.Configuration;
    using System.IO;
    using System.Xml.Serialization;
    using NewYearsGift.Classes;

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

            XmlSerializer xml = new XmlSerializer(typeof(Gift), new Type[] { typeof(Toy), typeof(Box), typeof(Fruit), typeof(Candy) });
            Gift gift = null;

            if (!File.Exists(path))
            {
                Console.WriteLine(String.Format("File {0} does not exist. It will be created and filled with predefined data.", path));
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
                    gift = (Gift)xml.Deserialize(reader);
                }
            }

            Console.WriteLine(String.Format("Gift name: {0}", gift.Name));
            Console.WriteLine(String.Format("Gift article: {0}", gift.ProductArticle));
            Console.WriteLine(String.Format("Gift consists from {0} items.", gift.Items.Count));
            Console.WriteLine(String.Format("Gift weight: {0}", gift.Weight));
        }
    }
}
