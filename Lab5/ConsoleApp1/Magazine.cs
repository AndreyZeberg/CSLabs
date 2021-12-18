using System;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace ConsoleApp1
{
    [Serializable]
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

        //public object DeepCopy()
        //{
        //    Magazine m = new Magazine(editionName, new DateTime(release.Year, release.Month, release.Day), copies, freq);
        //    foreach (Article a in articles)
        //    {
        //        m.articles.Add((Article)a.DeepCopy());
        //    }
        //    foreach (Person p in editors)
        //    {
                
        //        m.editors.Add((Person)p.DeepCopy());
        //    }

        //    return m;
        //}

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

        public Magazine DeepCopy()
        {
            Magazine m = null;
            MemoryStream memorstream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                binaryFormatter.Serialize(memorstream, this);
                memorstream.Position = 0;
                m = (Magazine)binaryFormatter.Deserialize(memorstream);
            }
            catch (SerializationException serExc)
            {
                Console.WriteLine("Serialization Failed");
                Console.WriteLine(serExc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "The serialization operation failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
            }
            finally
            {
                memorstream.Dispose();
            }

            return m;
        }

        public bool Save(string filename)
        {
            if (string.IsNullOrEmpty(filename)) { return false; }

            bool fSuccess = true;
            Stream stream = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                stream = File.Open(filename, FileMode.Create);
                binaryFormatter.Serialize(stream, this);
            }
            catch (SerializationException serExc)
            {
                Console.WriteLine("Serialization Failed");
                Console.WriteLine(serExc.Message);
                fSuccess = false;
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "The serialization or open file operation failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
                fSuccess = false;
            }
            finally
            {
                stream.Dispose();
            }

            return fSuccess;
        }

        public bool Load(string filename)
        {
            if (string.IsNullOrEmpty(filename)) { return false; }

            bool fSuccess = true;
            Magazine m = null;
            Stream stream = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                m = (Magazine)binaryFormatter.Deserialize(stream);
            }
            catch (SerializationException serExc)
            {
                Console.WriteLine("Serialization Failed");
                Console.WriteLine(serExc.Message);
                fSuccess = false;
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "The serialization or open file operation failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
                fSuccess = false;
            }
            finally
            {
                stream.Dispose();
            }

            if(fSuccess)
            {
                freq = m.freq;
                editors = m.editors;
                articles = m.articles;
                editionName = m.editionName;
                release = m.release;
                copies = m.copies;
            }

            return fSuccess;
        }

        public static bool Save(string filename, Magazine obj)
        {
            if (string.IsNullOrEmpty(filename)) { return false; }

            bool fSuccess = true;
            Stream stream = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                stream = File.Open(filename, FileMode.Create);
                binaryFormatter.Serialize(stream, obj);
            }
            catch (SerializationException serExc)
            {
                Console.WriteLine("Serialization Failed");
                Console.WriteLine(serExc.Message);
                fSuccess = false;
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "The serialization or open file operation failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
                fSuccess = false;
            }
            finally
            {
                stream.Dispose();
            }

            return fSuccess;
        }
        public static bool Load(string filename, Magazine obj)
        {
            if (string.IsNullOrEmpty(filename)) { return false; }

            bool fSuccess = true;
            Magazine m = null;
            Stream stream = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                m = (Magazine)binaryFormatter.Deserialize(stream);
            }
            catch (SerializationException serExc)
            {
                Console.WriteLine("Serialization Failed");
                Console.WriteLine(serExc.Message);
                fSuccess = false;
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "The serialization or open file operation failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
                fSuccess = false;
            }
            finally
            {
                stream.Dispose();
            }

            if(fSuccess)
            {
                obj.freq = m.freq;
                obj.editors = m.editors;
                obj.articles = m.articles;
                obj.editionName = m.editionName;
                obj.release = m.release;
                obj.copies = m.copies;
            }

            return fSuccess;
        }

        public bool AddFromConsole()
        {
            bool fSuccess = true;

            Type[] typesOfParams =
                { typeof(string), typeof(double), typeof(string),
                typeof(string), typeof(DateTime)};
            int numParams = typesOfParams.Length;
            object[] Params = new object[numParams];

            Console.WriteLine("Enter new article parameters as follows " +
                "(delimiter between parameters is ' '):\n" +
                "[article-title] [article-rate] [author-name] [author-surname] [dd/mm/yyyy]");

            string line = Console.ReadLine();
            string[] strParams = line.Split(' ');
            try
            {
                for (int i = 0; i < numParams; ++i)
                {
                    System.ComponentModel.TypeConverter typeConverter =
                        System.ComponentModel.TypeDescriptor.GetConverter(typesOfParams[i]);
                    Params[i] = typeConverter.ConvertFromString(strParams[i]);
                }

                AddArticles(new Article
                    (new Person((string)Params[2], (string)Params[3], (DateTime)Params[4]),
                    (string)Params[0], (double)Params[1]));
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "AddFromConsole function failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
                fSuccess = false;
            }

            return fSuccess;
        }
    }
}