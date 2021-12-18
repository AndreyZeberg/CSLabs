using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class MagazineEnumerator : IEnumerator
    {
        public List<Article> articles;
        public List<Person> editors;

        int position = -1;

        public MagazineEnumerator(List<Article> articles, List<Person> editors)
        {
            this.articles = articles;
            this.editors = editors;
        }

        public bool MoveNext()
        {
            int size = articles.Count;
            bool find = false;
            for (int i = position + 1; i < size; ++i)
            {
                Article a = articles[i];
                bool t = true;

                foreach (Person p in editors)
                {
                    if (a.Person == p)
                    {
                        t = false;
                        break;
                    }
                }

                if (t)
                {
                    find = true;
                    position = i;
                    break;
                }
            }

            return find;
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Article Current
        {
            get
            {
                try
                {
                    return articles[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
