using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    [Serializable]
    class Person
    {
        string name;
        string surname;
        DateTime birthday;
        
        public Person(string nameValue, string surnameValue, DateTime birthdayValue)
        {
            name = nameValue;
            surname = surnameValue;
            birthday = birthdayValue;
        }

        public Person() : this("Иван", "Иванов", new DateTime(2000, 1, 1))
        {
        }

        public string Name { get => name; set => name = value; }

        public string Surname { get => surname; set => surname = value; }

        public DateTime Birthday { get => birthday; set => birthday = value; }

        public int Year { get => birthday.Year; set => Birthday = new DateTime(value, Birthday.Month, Birthday.Day); }

        public override string ToString()
        {
            return Name + " " + Surname + " " + Birthday.ToShortDateString();
        }

        public virtual string ToShortString()
        {
            return Name + " " + Surname;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Person p = (Person)obj;

                if (!name.Equals(p.name) ||
                    !surname.Equals(p.surname) ||
                    !birthday.Equals(p.birthday))
                {
                    return false;
                }

                return true;
            }
        }

        public static bool operator ==(Person p1, Person p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Person p1, Person p2)
        {
            return !p1.Equals(p2);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            unchecked
            {
                hash = hash * 486187739 + name.GetHashCode();
                hash = hash * 486187739 + surname.GetHashCode();
                hash = hash * 486187739 + birthday.GetHashCode();
            }
            return hash;
        }

        public virtual object DeepCopy()
        {
            return new Person(name, surname, new DateTime(birthday.Year, birthday.Month, birthday.Day));
        }
    }
}
