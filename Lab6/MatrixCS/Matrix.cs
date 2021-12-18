using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCS
{
	class Matrix
	{
		double[] col;

		public Matrix(int n)
		{
			if (n <= 0)
			{
				throw new Exception("Constructor Matrix(int n): n <= 0");
			}
			col = new double[n];
			col[0] = 10000.0;
			for (int i = 1; i < n; ++i)
			{
				col[i] = 1.0 * i / n * 4.0 + 6.0;
			}
		}

		public Matrix(double[] col)
		{
			if (col.Length == 0)
			{
				throw new Exception("Constructor Matrix(int n): n == 0");
			}
			this.col = col;
		}

		public double[] Solve(double[] colfree)
		{
			int num = col.Length;
			double[] x = new double[num];

			double temp = 0.0;
			int step;
			int pos;

			double F, r, s;

			if (col[0] == 0)
			{
				throw new Exception("Matrix::solve(double* pcolfree, double* pcolres): m_pcol[0] == 0");
			}

			x[0] = 1.0 / col[0];

			if(num > 1)
            {
				F = col[1] * x[0];
				r = 1.0 / (1.0 - F * F);
				s = -r * F;

				x[1] = s * x[0];
				x[0] = r * x[0];

				for (int k = 2; k < num; ++k)
				{
					F = 0.0;
					for (int i = 0; i < k; ++i)
					{
						F += col[k - i] * x[i];
					}
					r = 1.0 / (1.0 - F * F);
					s = -r * F;

					x[k] = s * x[0];
					x[0] = r * x[0];

					pos = k / 2;
					if (k % 2 == 0)
					{
						x[pos] = r * x[pos] + s * x[pos];
						--pos;
						step = -1;
					}
					else
					{
						step = 0;
					}
					for (; pos > 0; pos += step)
					{
						if (step > 0)
						{
							step = -step - 1;
							x[pos] = r * x[pos] + s * temp;
						}
						else
						{
							temp = x[pos];
							step = -step + 1;
							x[pos] = r * x[pos] + s * x[pos + step];
						}
					}
				}
			}

			double[] colres = new double[num];

			int ind;
			for (int i = 0; i < num; ++i)
			{
				colres[i] = 0.0;
				for (int j = 0; j < num; ++j)
				{
					ind = i - j >= 0 ? i - j : j - i;
					colres[i] += x[ind] * colfree[j];
				}
			}

			return colres;
		}

		public override string ToString()
		{
			int num = col.Length;
			string s = "";

			int ind;
			for (int i = 0; i < num; ++i)
			{
				for (int j = 0; j < num; ++j)
				{
					ind = i - j >= 0 ? i - j : j - i;
					s += col[ind].ToString() + '\t';
				}
				s += '\n';
			}

			return s;
		}
	}
}
