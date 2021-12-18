#pragma once

class Matrix
{
	double* m_pcol;
	int m_num;

public:
	Matrix(int n);
	Matrix(int n, double* pcol);
	Matrix(const Matrix&);

	void solve(double* pcolfree, double* pcolres);
	Matrix& operator=(const Matrix&);

	~Matrix();
};

