using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    [Serializable]
    class Article : IRateAndCopy
    {
        Person person;
        string title;
        double rate;

        public Article(Person personValue, string titleValue, double rateValue)
        {
            person = personValue;
            title = titleValue;
            rate = rateValue;
        }

        public Article()
        {
            person = new Person();
            title = "title1";
            rate = 10;
        }

        public Person Person { get => person; set => person = value; }

        public string Title { get => title; set => title = value; }

        public double Rating { get => rate; }

        public override string ToString()
        {
            return person.ToString() + " " + title + " " + rate;
        }

        public virtual object DeepCopy()
        {
            return new Article((Person)person.DeepCopy(), title, rate);
        }
    }
}
