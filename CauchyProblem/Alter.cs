using System;
using CauchyProblem.Problems;
using CauchyProblem.Problems.Parametrization;

namespace CauchyProblem
{
	public class Alter
	{
		private Dirihle_Neymana DN = null;
		private Neymana_Dirihle ND = null;
		
		private static decimal Noisy = 0.05M;
		
		public (decimal[],decimal[]) Procces(bool useNoisy)
		{
			int k = 0;

			var res1 = GetH();
			var res2 = GetF1_G1(useNoisy);
			
			var U_ND = new decimal[2*Parameters.M];
			var pochidna_DN = new decimal[2*Parameters.M];
			
			while (k!=2)
			{
				ND = new Neymana_Dirihle(res1,res2);
				
				res1 = ND.CalculateUG0();
				U_ND = ND.CalculateUG0();
				res2 = GetG_pochidnaG1();
				
				DN = new Dirihle_Neymana(res1,res2);

				res1 = DN.CalculatePochidnaG0();
				pochidna_DN = DN.CalculatePochidnaG0();
				res2 = GetF1_G1(useNoisy);
				k++;
			}
			
			return (U_ND,pochidna_DN);
		}
		
		private decimal[] GetF1_G1(bool useNoisy)
		{
			var res = new decimal[2 * Parameters.M];
			
			Random r = new Random();
			decimal randomValue = (decimal)r.Next(0, 100) / 100M;
			
			var norma = 0.0M;
			
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				res[i] = (decimal)(Math.Pow((double)Parametrization_ND.X11(t),2)-Math.Pow((double)Parametrization_ND.X12(t),2));
				norma += (res[i] * res[i]);
			}

			if (useNoisy)
			{
				for (int i = 0; i < res.Length; i++)
				{
					res[i] = res[i] + Noisy*(2M*randomValue - 1M) * (decimal)Math.Sqrt((double)norma);
				}
			}
			
			return res;
		}
		
		private decimal[] GetH()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				res[i] = 0M;
			}

			return res;
		}
		
		private static decimal[] GetG_pochidnaG1()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				var dod1 = 2M * Parametrization_ND.X11(t) * Parametrization_ND.X12d(t) / Normal.GetModul(new[]
				{
					Parametrization_ND.X11d(t),
					Parametrization_ND.X12d(t)
				});
				
				var dod2 = 2M * Parametrization_ND.X12(t) * Parametrization_ND.X11d(t) / Normal.GetModul(new[]
				{
					Parametrization_ND.X11d(t),
					Parametrization_ND.X12d(t)
				});

				res[i] = dod1 + dod2;
			}

			return res;
		}
	}
}