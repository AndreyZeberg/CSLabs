using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class MagazineListHandlerEventArgs : EventArgs
    {
        public MagazineListHandlerEventArgs(string name, string info, int number) 
        {
            Name = name;
            Info = info;
            Number = number;
        }

        public string Name { get; set; }
        public string Info{ get; set; }
        public int Number{ get; set; }

        public override string ToString()
        {
            return "Collection name: " + Name + '\n'
                + Info + '\n'
                + "Index: " + Number.ToString();
        }
    }
}
