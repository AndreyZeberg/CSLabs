using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Magazine m = new Magazine();
            m.AddArticles(new Article(), new Article(new Person(), "tITLE", 5.9), new Article(new Person("Alexander", "Daniil", new DateTime(1999, 6, 22)), "SCIENCE", 1.002));
            m.AddEditors(new Person(), new Person("Kiril", "Kirill", new DateTime(2005, 5, 10)));

            Magazine mcopy = m.DeepCopy();
            Console.WriteLine($"source:\n{m}");
            Console.WriteLine($"copy:\n{mcopy}");

            Console.Write("Enter file name: ");
            string filename = Console.ReadLine();
            Magazine mobj = new Magazine();

            try
            {
                if(File.Exists(filename))
                {
                    if(!mobj.Load(filename))
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    Console.WriteLine($"File {filename} not found");
                    if(!m.Save(filename))
                    {
                        throw new Exception();
                    }
                    if (!mobj.Load(filename))
                    {
                        throw new Exception();
                    }
                }
                Console.WriteLine(mobj);
                if(!mobj.AddFromConsole())
                {
                    throw new Exception();
                }
                if(!mobj.Save(filename))
                {
                    throw new Exception();
                }
                Console.WriteLine(mobj);

                if(!Magazine.Load(filename, mobj))
                {
                    throw new Exception();
                }
                if (!mobj.AddFromConsole())
                {
                    throw new Exception();
                }
                if (!Magazine.Save(filename, mobj))
                {
                    throw new Exception();
                }
                Console.WriteLine(mobj);
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "{0}\nstackTrace: {1}",
                exc.Message, exc.StackTrace);
            }

        }
    }
}