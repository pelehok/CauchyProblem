using System;
using CauchyProblem.Problems.Kernels;
using CauchyProblem.Problems.Parametrization;

namespace CauchyProblem.Problems
{
	public class Dirihle_Neymana
	{
		private decimal[,] matrixA { get; set; }
		private decimal[] vectorF { get; set; }
		
		private decimal[] t { get; set; }
		
		public Dirihle_Neymana(decimal[] h, decimal[] f1)
		{
			t = InitTVector();
			InitMatrix();
			InitF(h,f1);
		}

		public decimal[] CalculateDestiny()
		{
			GauseMethod gause = new GauseMethod();

			return gause.FindSolution(matrixA, vectorF);
		}

		public decimal[] InitTVector()
		{
			var res = new decimal[2*Parameters.M];

			for (int i = 0; i < 2 * Parameters.M; i++)
			{
				res[i] = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
			}

            return res;
        }

		public void InitMatrix()
		{
			matrixA = new decimal[4*Parameters.M,4*Parameters.M];

			for (int i = 0; i < 4 * Parameters.M; i++)
			{
				for (int j = 0; j < 4 * Parameters.M; j++)
				{
					if (i < 2 * Parameters.M)
					{
						if (j < 2 * Parameters.M)
						{
							matrixA[i, j] = Kernels_DN.H01(t[i], t[j]) / (2 * Parameters.M)
							                - Normal.R(t[j], t[i]) / 2M;
						}
						else
						{
							matrixA[i, j] = Kernels_DN.H02(t[i], t[j - 2 * Parameters.M]) /
							                (2 * Parameters.M);
						}
					}
					else
					{
						if (j < 2 * Parameters.M)
						{
							matrixA[i, j] = Kernels_DN.K11(t[i - 2 * Parameters.M], t[j]) /
							                (2 * Parameters.M);
						}
						else
						{
							if (i == j)
							{
								matrixA[i, j] = Kernels_DN.K21(t[i - 2 * Parameters.M], t[j - 2 * Parameters.M]) /
								                (2 * Parameters.M ) + GetDod1(j - 2 * Parameters.M);
							}
							else
							{
								matrixA[i, j] = Kernels_DN.K21(t[i - 2 * Parameters.M], t[j - 2 * Parameters.M]) /
								                (2 * Parameters.M);
							}
						}
					}
				}
			}
		}

		private decimal GetDod1(int j)
		{
			return  1 / (2M * Normal.GetModul(new[]
			{
				Parametrization_ND.X11d(t[j]),
				Parametrization_ND.X12d(t[j])
			}));
		}

		public void InitF(decimal[] h,decimal[] f1)
		{
			vectorF = new decimal[4*Parameters.M];

			for (int i = 0; i < vectorF.Length; i++)
			{
				if (i < 2 * Parameters.M)
				{
					vectorF[i] = h[i];
				}
				else
				{
					vectorF[i] = f1[i-2*Parameters.M];
				}
			}
		}

		public decimal CalculateU(decimal[] dectiny, decimal[] x)
		{
			decimal sum1 = 0;
			decimal sum2 = 0;
			for (int k = 0; k < 2 * Parameters.M; k++)
			{
				sum1 += dectiny[k] * Kernels_ND.Fundamental(x, new []{Parametrization_ND.X01(t[k]),Parametrization_ND.X02(t[k])});
				sum1 += dectiny[k + 2 * Parameters.M] * Kernels_ND.Fundamental(x, new []{Parametrization_ND.X11(t[k]),Parametrization_ND.X12(t[k])});
			}

			return (sum1 + sum2)/(2M*Parameters.M);
		}
	}
}