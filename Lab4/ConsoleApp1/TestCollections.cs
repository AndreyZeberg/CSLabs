using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    class TestCollections
    {
        List<Edition> list;
        List<string> strList;
        Dictionary<Edition, Magazine> dictionary;
        Dictionary<string, Magazine> strDictionary;

        int size;

        public TestCollections(int num)
        {
            size = num;
            list = new List<Edition>(num);
            strList = new List<string>(num);
            dictionary = new Dictionary<Edition, Magazine>(num);
            strDictionary = new Dictionary<string, Magazine>(num);
        }

        public void Start()
        { 
            for(int i = 0; i < size; ++i)
            {
                Magazine m = Put(i);
                Edition e = m.Edition;
                list.Add(e);
                dictionary.Add(e, m);
                strList.Add(e.ToString());
                strDictionary.Add(e.ToString(), m);
            }

            TimeSpan[,] tsInfo = new TimeSpan[4, 5];

            Stopwatch sw = new Stopwatch();
            for(int i = 0; i < 4; ++i)
            {
                int ii = 0;
                switch (i)
                {
                    case 0:
                        ii = i;
                        break;

                    case 1:
                        ii = size / 2;
                        break;

                    case 2:
                        ii = size - 1;
                        break;

                    case 3:
                        ii = size + 1;
                        break;
                }
                Magazine m = Put(ii);
                Edition e = m.Edition;
                string es = e.ToString();
                for(int j = 0; j < 5; ++j)
                {
                    switch(j)
                    {
                        case 0:
                            sw.Start();
                            list.Contains(e);
                            sw.Stop();
                            break;

                        case 1:
                            sw.Start();
                            strList.Contains(es);
                            sw.Stop();
                            break;

                        case 2:
                            sw.Start();
                            dictionary.ContainsKey(e);
                            sw.Stop();
                            break;

                        case 3:
                            sw.Start();
                            strDictionary.ContainsKey(es);
                            sw.Stop();
                            break;

                        case 4:
                            sw.Start();
                            dictionary.ContainsValue(m);
                            sw.Stop();
                            break;
                    }
                    tsInfo[i, j] = sw.Elapsed;
                    sw.Reset();
                }
            }

            //print info
            string[] row =
            {
                "First",
                "Central",
                "Last",
                "Out-of_coll",
            };
            string[] col =
            {
                "list.Contains()",
                "strList.Contains()",
                "dictionary.ContainsKey()",
                "strDictionary.ContainsKey()",
                "dictionary.ContainsValue()",
            };
            int indent = 12;
            Console.Write(new string(' ', indent));
            foreach(string s in col)
            {
                Console.Write(s + ' ');
            }
            Console.WriteLine();
            for (int i = 0; i < 4; ++i)
            {
                Console.Write((row[i]).PadRight(indent));
                for (int j = 0; j < 5; ++j)
                {
                    Console.Write(
                        (tsInfo[i, j].TotalMilliseconds.ToString() + "ms")
                        .PadRight(col[j].Length + 1)
                        );
                }
                Console.WriteLine();
            }
        }

        public static Magazine Put(int i)
        {
            return new Magazine(i.ToString(),
                new DateTime(1919, 1, 1),
                i + 1,
                (Frequency)(i % 3));
        }
    }
}
