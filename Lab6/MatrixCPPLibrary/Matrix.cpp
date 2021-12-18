#include "pch.h"
#include "Matrix.h"

Matrix::Matrix(int n)
{
	if (n <= 0)
	{
		throw std::exception("Constructor Matrix(int n): n <= 0");
	}

	m_pcol = new double[n];
	m_num = n;

	m_pcol[0] = 10000.0;
	for (int i{ 1 }; i < n; ++i)
	{
		m_pcol[i] = 1.0 * i / n * 4.0 + 6.0;
	}
}

Matrix::Matrix(int n, double* pcol)
{
	if (n <= 0)
	{
		throw std::exception("Constructor Matrix(int n): n <= 0");
	}
	m_pcol = new double[n];
	m_num = n;

	for (int i{}; i < n; ++i)
	{
		m_pcol[i] = pcol[i];
	}
}

Matrix::Matrix(const Matrix& m)
{
	m_num = m.m_num;
	m_pcol = new double[m_num];

	double* pcol = m.m_pcol;
	for (int i{}; i < m_num; ++i)
	{
		m_pcol[i] = pcol[i];
	}
}

void Matrix::solve(double* pcolfree, double* pcolres)
{
	double* x = new double[m_num];

	double temp;
	int step;
	int pos;

	double F, r, s;

	if (m_pcol[0] == 0)
	{
		throw std::exception("Matrix::solve(double* pcolfree, double* pcolres): m_pcol[0] == 0");
	}
	x[0] = 1.0 / m_pcol[0];

	if (m_num > 1)
	{
		F = m_pcol[1] * x[0];
		r = 1.0 / (1.0 - F * F);
		s = -r * F;

		x[1] = s * x[0];
		x[0] = r * x[0];

		for (int k{ 2 }; k < m_num; ++k)
		{
			F = 0.0;
			for (int i{}; i < k; ++i)
			{
				F += m_pcol[k - i] * x[i];
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

	int ind;
	for (int i{}; i < m_num; ++i)
	{
		pcolres[i] = 0.0;
		for (int j{}; j < m_num; ++j)
		{
			ind = i - j >= 0 ? i - j : j - i;
			pcolres[i] += x[ind] * pcolfree[j];
		}
	}

	delete[] x;
}

Matrix& Matrix::operator=(const Matrix& m)
{
	if (this == &m)
		return *this;
	delete[] m_pcol;
	m_num = m.m_num;
	m_pcol = new double[m_num];

	double* pcol = m.m_pcol;
	for (int i{}; i < m_num; ++i)
	{
		m_pcol[i] = pcol[i];
	}

	return *this;
}

Matrix::~Matrix()
{
	delete[] m_pcol;
}
