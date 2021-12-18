using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MagazineCollection magColl1 = new MagazineCollection(("magColl1"));
            MagazineCollection magColl2 = new MagazineCollection(("magColl2"));

            Listener listener1 = new Listener();
            Listener listener2 = new Listener();

            magColl1.MagazineAdded += listener1.AddListEntry;
            magColl1.MagazineReplaced += listener1.AddListEntry;
            magColl1.MagazineAdded += listener2.AddListEntry;
            magColl2.MagazineAdded += listener2.AddListEntry;

            magColl1.AddDefaults();
            magColl2.AddDefaults();

            Magazine tm = new Magazine("hELLO", new DateTime(2009, 1, 7), 83200, Frequency.Weekly);
            tm.AddArticles(new Article(new Person(), "TITLE", 8.4224), new Article(new Person("Zakharova", "Alisa", new DateTime(2001, 1, 1)), "Rainier Fog", 8.1));
            tm.AddEditors(new Person("Fedor", "Kuznetsov", new DateTime(1988, 12, 5)));
            magColl1.Replace(0, tm);
            tm = new Magazine("Cell Biology", new DateTime(2018, 12, 1), 25000, Frequency.Yearly);
            tm.AddArticles(new Article(new Person(), "TITLE", 8.4224), new Article(new Person("Alexander", "Daniil", new DateTime(1980, 9, 13)), "Arminius, Jacob", 2.2));
            tm.AddEditors(new Person(), new Person("Kiril", "Kirill", new DateTime(2005, 5, 10)));
            magColl2.Replace(1, tm);

            tm = new Magazine("Cell Biology", new DateTime(2018, 12, 1), 25000, Frequency.Yearly);
            tm.AddArticles(new Article(new Person(), "TITLE", 8.4224), new Article(new Person("Alexander", "Daniil", new DateTime(1980, 9, 13)), "Arminius, Jacob", 2.2));
            tm.AddEditors(new Person(), new Person("Kiril", "Kirill", new DateTime(2005, 5, 10)));
            magColl1[1] = tm;
            tm = new Magazine("hELLO", new DateTime(2009, 1, 7), 83200, Frequency.Weekly);
            tm.AddArticles(new Article(new Person(), "TITLE", 8.4224), new Article(new Person("Zakharova", "Alisa", new DateTime(2001, 1, 1)), "Rainier Fog", 8.1));
            tm.AddEditors(new Person("Fedor", "Kuznetsov", new DateTime(1988, 12, 5)));
            magColl2[2] = tm;

            Console.WriteLine(listener1);            
            Console.WriteLine(listener2);            
        }
    }
}