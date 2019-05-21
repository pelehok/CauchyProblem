using System;
using CauchyProblem.Problems;
using CauchyProblem.Problems.Parametrization;

namespace CauchyProblem
{
	public class Alter
	{
		private Dirihle_Neymana DN = null;
		private Neymana_Dirihle ND = null;
		
		public (decimal[],decimal[]) Procces()
		{
			int k = 0;

			var res1 = GetH();
			var res2 = GetF1_G1();
			
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
				res2 = GetF1_G1();
				k++;
			}
			
			return (U_ND,pochidna_DN);
		}
		
		private decimal[] GetF1_G1()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				res[i] = (decimal)(Math.Pow((double)Parametrization_ND.X11(t),2)-Math.Pow((double)Parametrization_ND.X12(t),2));
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