using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MagazineCollection magColl = new MagazineCollection();
            magColl.AddDefaults();
            Console.Write(magColl);

            Console.Write("\nSorted by count:\n");
            magColl.SortByCount();
            Console.Write(magColl);

            Console.Write("\nSorted by name:\n");
            magColl.SortByName();
            Console.Write(magColl);

            Console.Write("\nSorted by release:\n");
            magColl.SortByRelease();
            Console.Write(magColl);


            double r = magColl.MaxAverageRate;
            Console.WriteLine($"Max average rate: {0}", r);

            Console.WriteLine("\nList monthly: ");
            MagazineCollection t = new MagazineCollection();
            t.Magazines = new System.Collections.Generic.List<Magazine>(magColl.ListMonthly);
            Console.WriteLine(t);

            Console.WriteLine("\nRatingGroup: ");
            t = new MagazineCollection();
            t.Magazines = magColl.RatingGroup(7.0);
            Console.WriteLine(t);

            int numEl = 1000;
            string input = "";

            Console.Write("Введите кол-во элементов: ");
            while (true)
            {
                input = Console.ReadLine();
                if (Int32.TryParse(input, out numEl))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод, необходимо ввести число");
                }
            }
            Console.WriteLine($"Кол-во элементов: {numEl}");

            TestCollections Test = new TestCollections(numEl);
            Test.Start();
        }
    }
}