using System;
namespace ExampleIEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> mylist = new MyList<int>();
            mylist.Add(1);
            mylist.Add(2);
            mylist.Add(3);

            mylist.Remove(2);

            foreach (var n in mylist)
            {
                Console.WriteLine(n);
            }

            MyList<string> mylist2 = new MyList<string>();
            mylist2.Add("1");
            mylist2.Add("2");
            mylist2.Add("3");

            foreach (var n in mylist2)
            {
                Console.WriteLine(n);
            }

            MyList<Test> list555 = new MyList<Test>();
            Test b1 = new Test() { a = "1", b = 11 };
            Test b2 = new Test() { a = "2", b = 22 };
            Test b3 = new Test() { a = "3", b = 33 };

            list555.Add(b1);
            list555.Add(b2);
            list555.Add(b3);

            foreach (var n in list555)
            {
                Console.WriteLine(String.Format("a = {0} b = {1}", n.a, n.b));
            }

            Console.WriteLine(list555.Contains(b2) == true);
            list555.Remove(b2);
            Console.WriteLine(list555.Contains(b2) == true);
            
            Console.ReadKey();
        }
    }

    public class Test
    {
        public string a { get; set; }
        public int b { get; set; }
    }
}
