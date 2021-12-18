using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;

namespace ConsoleApp1
{
    class MagazineCollection
    {
        List<Magazine> list;

        public MagazineCollection()
        {
            list = new List<Magazine>();
        }

        public void AddDefaults()
        {
            list.AddRange(new List<Magazine>(5) { null, null, null, null, null });
            list[0] = new Magazine();
            list[0].AddArticles(new Article(), new Article(new Person(), "TITLE", 8.4224), new Article(new Person("Alexander", "Daniil", new DateTime(1999, 6, 22)), "SCIENCE", 1.002));
            list[0].AddEditors(new Person("Luka", "Semyonov", new DateTime(2000, 2, 20)));
            list[1] = new Magazine("IXBT.COM", new DateTime(2002, 6, 1), 40000, Frequency.Monthly);
            list[1].AddArticles(new Article(new Person(), "TITLE", 8.4224));
            list[1].AddEditors(new Person("Rozanov", "Alexey", new DateTime(1983, 7, 11)));
            list[2] = new Magazine("HELLO", new DateTime(2009, 1, 6), 83200, Frequency.Weekly);
            list[2].AddArticles(new Article(new Person(), "TITLE", 8.4224), new Article(new Person("Alexander", "Tretyakov", new DateTime(1995, 6, 22)), "Star", 6.7));
            list[2].AddEditors(new Person("Daria", "Kalashnikova", new DateTime(1999, 2, 27)));
            list[3] = new Magazine("hELLO", new DateTime(2009, 1, 7), 83200, Frequency.Weekly);
            list[3].AddArticles(new Article(new Person(), "TITLE", 8.4224), new Article(new Person("Zakharova", "Alisa", new DateTime(2001, 1, 1)), "Rainier Fog", 8.1));
            list[3].AddEditors(new Person("Fedor", "Kuznetsov", new DateTime(1988, 12, 5)));
            list[4] = new Magazine("Cell Biology", new DateTime(2018, 12, 1), 25000, Frequency.Yearly);
            list[4].AddArticles(new Article(new Person(), "TITLE", 8.4224), new Article(new Person("Alexander", "Daniil", new DateTime(1980, 9, 13)), "Arminius, Jacob", 2.2));
            list[4].AddEditors(new Person(), new Person("Kiril", "Kirill", new DateTime(2005, 5, 10)));
        }

        public void AddMagazines(params Magazine[] mlist)
        {
            list.AddRange(mlist);
        }

        public override string ToString()
        {
            string str = "";
            foreach (Magazine m in list)
            {
                str += m.ToString() + '\n';
            }

            return str;
        }

        public virtual string ToShortString()
        {
            string str = "";
            foreach (Magazine m in list)
            {
                str += '\"' + m.EditionName + '\"' + " " + m.Freq.ToString() + " "
                + m.Release.ToShortDateString() + " " + m.Copies + '\n'
                + "Количество редакторов журнала: " + m.Editors.Count + '\n'
                + "Количество статей: " + m.Articles.Count + '\n'
                + "Средний рейтинг статей: " + m.Rating + "\n\n";
            }

            return str;
        }

        public List<Magazine> Magazines
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }

        public List<Magazine> ListByName
        {
            get
            {
                list.Sort();
                return list;
            }
        }

        public List<Magazine> ListByRelease
        {
            get
            {
                list.Sort(new Edition());
                return list;
            }
        }

        public List<Magazine> ListByCount
        {
            get
            {
                list.Sort(new EditionComparer());
                return list;
            }
        }


        public void SortByName()
        {
            list.Sort();
        }

        public void SortByRelease()
        {
            list.Sort(new Edition());
        }

        public void SortByCount()
        {
            list.Sort(new EditionComparer());
        }

        public double MaxAverageRate
        {
            get
            {
                double max;
                if (list.Count != 0)
                {
                    max = list.Max(m => m.Rating);
                }
                else
                {
                    max = 6.0;
                }

                return max;
            }
        }

        public IEnumerable<Magazine> ListMonthly
        {
            get
            {
                return list.Where(m => m.Freq == Frequency.Monthly);
            }
        }

        public List<Magazine> RatingGroup(double value)
        {
            var groups = list.GroupBy(m => m.Rating)
                .Where(g => g.Key >= value);

            IEnumerable<Magazine> l = new List<Magazine>();
            foreach(var g in groups)
            {
                l = l.Concat(g.ToList());
            }

            return l.ToList();
        }
    }
}