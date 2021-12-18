using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace MatrixCS
{
    class Program
    {
        const string path = "MatrixCPPLibrary.dll";
        [DllImport(path, CallingConvention = CallingConvention.Cdecl)]
        public static extern double defalut_test(int n, int rep);
        [DllImport(path, CallingConvention = CallingConvention.Cdecl)]
        public static extern double test(int n, double[] col, int rep, double[] colfree, double[] colres);
        [DllImport(path, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Throws();
        static void Main(string[] args)
        {
            const int n = 3;
            double[] col = new double[n]{ 10000.0, 5.0, 6.0 };
            Matrix matr = new Matrix(col);
            double[] f = new double[n] { 2.5, 25.0, 12.5 };
            double[] r1 = null;
            double[] r2 = new double[n];
            try
            {
                r1 = matr.Solve(f);
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "{0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
                Environment.Exit(1);
            }
            test(n, col, 1, f, r2);

            Console.WriteLine("matr:\n" + matr);
            Console.WriteLine("f = (" + VecToString(f) + ')');
            Console.WriteLine("r1 = (" + VecToString(r1) + ')');

            Console.WriteLine("Result from C++ code:\nr2 = (" + VecToString(r2) + ')');

            Console.Write("Enter file name: ");
            string filename = Console.ReadLine();
            TimesList tl = new TimesList();

            try
            {
                if (File.Exists(filename))
                {
                    if (!tl.Load(filename))
                    {
                        throw new Exception();
                    }
                    Console.WriteLine(tl.ToString());
                }
                
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "{0}\nstackTrace: {1}",
                exc.Message, exc.StackTrace);
            }

            while (true)
            {
                Console.WriteLine("Enter the order of the matrix and the " +
                    "number of repetitions\n(type 'q' to exit).");

                string command = Console.ReadLine();
                string[] userinput = command.Split(' ');
                if (userinput[0] == "q")
                {
                    break;
                }

                if(userinput.Length != 2)
                {
                    continue;
                }
                int num, rep;
                if (!Int32.TryParse(userinput[0], out num))
                {
                    Console.WriteLine("Failed. Order is an integer.");
                    continue;
                }
                if (!Int32.TryParse(userinput[1], out rep))
                {
                    Console.WriteLine("Failed. Repetitions is an integer.");
                    continue;
                }

                TimeItem ti = new TimeItem();
                ti.CPPTime = defalut_test(num, rep);
                ti.CSharpTime = CSharpDefaultTest(num, rep);
                ti.Rep = rep;
                ti.MatrixOrder = num;

                tl.Add(ti);
            }

            Console.WriteLine("Time data:");
            Console.WriteLine(tl);

            Console.WriteLine($"Saving {filename}...");
            if (tl.Save(filename))
            {
                Console.WriteLine($"{filename} saved!");
            }
            else
            {
                Console.WriteLine($"{filename} not saved!");
            }
        }

        public static double CSharpDefaultTest(int n, int rep)
        {

            Matrix matrix = new Matrix(n);
            double[] colfree = new double[n];
            
            Random rnd = new Random();
            for (int i = 0; i < n; ++i)
            {
                colfree[i] = 1.0 * rnd.Next(0, 1) * 90.0 + 10.0;
            }

            Stopwatch sw = new Stopwatch();
            double[] res = null;

            sw.Start();
            for (int i = 0; i < rep; i++)
                res = matrix.Solve(colfree);
            sw.Stop();

            return sw.ElapsedMilliseconds;
        }

        public static string VecToString(double[] col)
        {
            int num = col.Length;
            string s = "";

            for (int i = 0; i < num - 1; ++i)
            {
                s += col[i].ToString() + "; ";
            }
            s += col[num - 1].ToString() + "";

            return s;
        }
    }
}
