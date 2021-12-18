using System;
using System.Collections.Generic;
using System.Linq;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace MatrixCS
{
    class TimesList
    {
        List<TimeItem> list;

        public TimesList()
        {
            list = new List<TimeItem>();
        }

        public void Add(TimeItem timeItem)
        {
            list.Add(timeItem);
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
                binaryFormatter.Serialize(stream, list);

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
            List<TimeItem> l = null;
            Stream stream = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                l = (List<TimeItem>)binaryFormatter.Deserialize(stream);
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

            if (fSuccess)
            {
                list = l;
            }

            return fSuccess;
        }

        public override string ToString()
        {
            string s = string.Format("{0,-7} {1,-10} {2,-20} {3,-10} {4,-11}\n",
                "CPPtime", "CSharpTime", "Ratio", "MarixOrder", "Repetitions");
            foreach (var item in list)
            {
                s += string.Format(
                    "{0,-7} " +
                    "{1,-10} " +
                    "{2,-20} " +
                    "{3,-10} " +
                    "{4,-11}\n",
                    item.CPPTime,
                    item.CSharpTime,
                    item.Ratio,
                    item.MatrixOrder,
                    item.Rep);
            }

            return s;
        }
    }
}
           