#pragma once

#ifdef MATRIXCPPLIBRARY_EXPORTS
#define MATRIXCPPLIBRARY_API __declspec(dllexport)
#else
#define MATRIXCPPLIBRARY_API __declspec(dllimport)
#endif

extern "C" MATRIXCPPLIBRARY_API double defalut_test(int n, int rep);

extern "C" MATRIXCPPLIBRARY_API double test(int n, double* col, int rep,
	double* colfree, double* colres);