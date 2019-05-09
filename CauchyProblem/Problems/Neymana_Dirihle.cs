using System;
using CauchyProblem.Problems.Kernels;

namespace CauchyProblem.Problems
{
	public class Neymana_Dirihle
	{
		private decimal[,] matrixA { get; set; }
		private decimal[] vectorF { get; set; }
		
		private decimal[] t { get; set; }
		
		public Neymana_Dirihle(decimal[] h, decimal[] f1)
		{
			InitTVector();
			InitMatrix();
			InitF(h,f1);
		}

		public decimal[] CalculateDestiny()
		{
			GauseMethod gause = new GauseMethod();

			return gause.FindSolution(matrixA, vectorF);
		}

		public void InitTVector()
		{
			t = new decimal[2*Parameters.M];

			for (int i = 0; i < t.Length; i++)
			{
				t[i] = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
			}
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
							if (i == j)
							{
								matrixA[i, j] = Kernels_ND.K01(t[i], t[j]) /
								                (2 * Parameters.M * GetDod1(j));
							}
							else
							{
								matrixA[i, j] = Kernels_ND.K01(t[i], t[j]) /
								                (2 * Parameters.M);
							}
						}
						else
						{
							matrixA[i, j] = Kernels_ND.K02(t[i], t[j - 2 * Parameters.M]) /
							                (2 * Parameters.M);
						}
					}
					else
					{
						if (j < 2 * Parameters.M)
						{
							matrixA[i, j] = Kernels_ND.P11(t[i - 2 * Parameters.M], t[j]) /
							                (2 * Parameters.M);
						}
						else
						{
							matrixA[i, j] = -Normal.R(t[j - 2 * Parameters.M], t[i - 2 * Parameters.M])/2M +
							                Kernels_ND.P12_2(t[i - 2 * Parameters.M], t[j - 2 * Parameters.M])
							                / (2 * Parameters.M);
						}
					}
				}
			}
		}

		private decimal GetDod1(int j)
		{
			return 2M * Normal.GetModul(new[]
			{
				Parametrization.Parametrization_ND.X01d(t[j]),
				Parametrization.Parametrization_ND.X02d(t[j])
			});
		}

		public void InitF(decimal[] h,decimal[] f1)
		{
			vectorF = new decimal[4*Parameters.M];

			for (int i = 0; i < t.Length; i++)
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

		public decimal[] CalculateU(decimal[] dectiny)
		{
			var res = new decimal[2*Parameters.M];
			for (int j = 0; j < res.Length; j++)
			{
				decimal sum1 = 0;
				decimal sum2 = 0;
				for (int k = 0; k < 2 * Parameters.M; k++)
				{
					sum1 += (dectiny[j] * Kernels_ND.P11(t[j], t[k]));
					sum2 += (dectiny[j + 2 * Parameters.M] *
					         -0.5M * Normal.R(t[k], t[j]) +
					         Kernels_ND.P12_2(t[j], t[k]) / (2 * Parameters.M));
				}

				res[j] = (sum1 + sum2) / (2M * Parameters.M);
			}

			return res;
		}
	}
}