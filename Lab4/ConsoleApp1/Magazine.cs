using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        Frequency freq;
        List<Person> editors;
        List<Article> articles;
        public Magazine() : base("Name1", new DateTime(2008, 5, 1), 200000)
        {
            freq = Frequency.Monthly;
            editors = new List<Person>();
            articles = new List<Article>();
        }

        public Magazine(
            string editionName,
            DateTime release,
            int copies,
            Frequency freq
            ) : base(editionName, release, copies)
        {
            this.freq = freq;
            editors = new List<Person>();
            articles = new List<Article>();
        }

        public Frequency Freq
        {
            get
            {
                return freq;
            }
            set
            {
                freq = value;
            }
        }

        public List<Article> Articles
        {
            get
            {
                return articles;
            }
            set
            {
                articles = value;
            }
        }

        public List<Person> Editors
        {
            get
            {
                return editors;
            }
            set
            {
                editors = value;
            }
        }

        public Edition Edition
        {
            get
            {
                return new Edition(editionName, new DateTime(release.Year, release.Month, release.Day), copies);
            }
            set
            {
                editionName = value.EditionName;
                release = new DateTime(value.Release.Year, value.Release.Month, value.Release.Day);
                copies = value.Copies;
            }
        }
        public double Rating
        {
            get
            {
                double sum = 0;
                foreach (Article article in articles)
                {
                    sum += article.Rating;
                }

                return sum / articles.Count;
            }
        }

        public bool this [Frequency fr]
        {
            get
            {
                return freq == fr;
            }
        }

        public void AddArticles(params Article[] list)
        {
            articles.AddRange(list);
        }

        public void AddEditors(params Person[] list)
        {
            editors.AddRange(list);
        }

        public override string ToString()
        {
            string editors_str = "";
            foreach(Person p in editors)
            {
                editors_str += "\t\t" + p.ToString() + '\n';
            }

            string articles_str = "";
            foreach(Article a in articles)
            {
                articles_str += "\t\t" + a.ToString() + '\n';
            }

            return '\"' + editionName + '\"' + " " + freq.ToString() + " "
                + release.ToShortDateString() + " " + copies + '\n'
                + "\tСписок редакторов журнала:\n" + editors_str
                + "\tСписок статей:\n" + articles_str;
        }

        public virtual string ToShortString()
        {
            return '\"' + editionName + '\"' + " " + freq.ToString() + " "
                + release.ToShortDateString() + " " + copies + " " + Rating;
        }

        public object DeepCopy()
        {
            Magazine m = new Magazine(editionName, new DateTime(release.Year, release.Month, release.Day), copies, freq);
            foreach (Article a in articles)
            {
                m.articles.Add((Article)a.DeepCopy());
            }
            foreach (Person p in editors)
            {
                
                m.editors.Add((Person)p.DeepCopy());
            }

            return m;
        }

        public IEnumerable ByRating(double minRate)
        {
            foreach(Article a in articles)
            {
                if(a.Rating > minRate)
                {
                    yield return a;
                }
            }
        }

        public IEnumerable BySubString(string SubString)
        {
            foreach (Article a in articles)
            {
                if (a.Title.Contains(SubString))
                    yield return a;
            }
        }

        public IEnumerable ArticleWithAuthorsWhoEditor()
        {
            foreach (Article a in articles)
            {
                foreach (Person p in editors)
                {
                    if (a.Person == p)
                    {
                        yield return a;
                    }
                }
            }
        }

        public IEnumerable EditorsWithoutArticle()
        {
            foreach (Person p in editors)
            {
                bool t = true;

                foreach (Article a in articles)
                {
                    if (a.Person == p)
                    {
                        t = false;
                        break;
                    }
                }

                if(t)
                {
                    yield return p;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(articles, editors);
        }

        //public IEnumerator GetEnumerator()
        //{
        //    List<Article> list = new List<Article>();

        //    foreach (Article a in articles)
        //    {
        //        bool t = true;

        //        foreach (Person p in editors)
        //        {
        //            if (a.Person == p)
        //            {
        //                t = false;
        //                break;
        //            }
        //        }

        //        if(t)
        //        {
        //            list.Add(a);
        //        }
        //    }

        //    return new MagazineEnumerator(list);
        //}
    }
}