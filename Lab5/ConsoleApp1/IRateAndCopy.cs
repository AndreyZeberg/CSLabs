using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    interface IRateAndCopy
    {
        double Rating { get; }

        object DeepCopy();
    }
}
