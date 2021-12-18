using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class EditionComparer : IComparer<Edition>
    {
        public int Compare(Edition e1, Edition e2)
        {
            return e1.Copies.CompareTo(e2.Copies);
        }
    }
}
