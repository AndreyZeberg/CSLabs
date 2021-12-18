#include "pch.h"
#include "Matrix.h"
#include "MatrixCPPLibrary.h"

double defalut_test(int n, int rep)
{
	int start, stop;
	double* colres = nullptr;
	double* colfree = nullptr;

	try
	{
		Matrix matrix(n);
		colres = new double[n];
		colfree = new double[n];
		for (int i{}; i < n; ++i)
		{
			colfree[i] = 1.0 * rand() / RAND_MAX * 90.0 + 10.0;
		}

		start = clock();
		for (int i{}; i < rep; ++i)
		{
			matrix.solve(colfree, colres);
		}
		stop = clock();
	}
	catch (const std::exception& exc)
	{
		delete[] colres;
		delete[] colfree;
		std::cout << exc.what();
		exit(1);
	}

	delete[] colres;
	delete[] colfree;

	return (double)(stop - start) / CLOCKS_PER_SEC * 1000;
}

double test(int n, double* col, int rep,
	double* colfree, double* colres)
{
	int start, stop;

	try
	{
		Matrix matrix(n, col);
		start = clock();
		for (int i{}; i < rep; ++i)
		{
			matrix.solve(colfree, colres);
		}
		stop = clock();
	}
	catch (const std::exception& ex)
	{
		std::cout << ex.what();
		exit(1);
	}

	return (double)(stop - start) / CLOCKS_PER_SEC * 1000;
}