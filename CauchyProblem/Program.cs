using System;
using CauchyProblem.Problems;
using CauchyProblem.Problems.Parametrization;

namespace CauchyProblem
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Neymana_Dirihle problem = new Neymana_Dirihle(GetH(), GetF1_1());

			var nablRes = problem.CalculateU(problem.CalculateDestiny());

			Console.WriteLine(Norma(nablRes,GetF1_0()));
		}

		private static decimal Norma(decimal[] vect1, decimal[] vect2)
		{
			decimal sum = 0;

			for (int i = 0; i < vect1.Length; i++)
			{
				sum += Math.Abs(vect1[i] - vect2[i]);
			}

			return sum;
		}

		private static decimal[] GetH()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				var dod1 = 2M * Parametrization_ND.X01(t) * Parametrization_ND.X02d(t) / Normal.GetModul(new[]
				{
					Parametrization_ND.X01d(t),
					Parametrization_ND.X02d(t)
				});
				
				var dod2 = 2M * Parametrization_ND.X02(t) * Parametrization_ND.X01d(t) / Normal.GetModul(new[]
				{
					Parametrization_ND.X01d(t),
					Parametrization_ND.X02d(t)
				});

				res[i] = dod1-dod2;
			}

			return res;
		}

		private static decimal[] GetF1_1()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				res[i] = (decimal)(Math.Pow((double)Parametrization_ND.X11(t),2)+Math.Pow((double)Parametrization_ND.X12(t),2));
			}

			return res;
		}
		
		private static decimal[] GetF1_0()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				res[i] = (decimal)(Math.Pow((double)Parametrization_ND.X01(t),2)+Math.Pow((double)Parametrization_ND.X02(t),2));
			}

			return res;
		}
	}
}