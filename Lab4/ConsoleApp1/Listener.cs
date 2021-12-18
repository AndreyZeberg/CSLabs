using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Listener
    {
        List<ListEntry> listOfChanges;

        public Listener()
        {
            listOfChanges = new List<ListEntry>();
        }

        public void AddListEntry(object source, MagazineListHandlerEventArgs args)
        {
            listOfChanges.Add(new ListEntry(args.Name, args.Info, args.Number));
        }

        public override string ToString()
        {
            string s = "";
            foreach(ListEntry le in listOfChanges)
            {
                s += le.ToString() + '\n';
            }

            return s;
        }
    }
}
