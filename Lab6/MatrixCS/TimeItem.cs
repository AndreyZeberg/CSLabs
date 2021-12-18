using System;

namespace MatrixCS
{
    [Serializable]
    class TimeItem
    {
        public int MatrixOrder { get; set; }
        public int Rep { get; set; }
        public double CSharpTime { get; set; }
        public double CPPTime { get; set; }
        public double Ratio
        {
            get
            {
                if (CPPTime != 0)
                    return CSharpTime / CPPTime;
                else
                    return -1;
            }
        }
    }
}