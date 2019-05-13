using System;
using System.Windows.Forms;
using CauchyProblem.Problems;
using CauchyProblem.Problems.Parametrization;
using Charts;

namespace CauchyProblem
{
	internal class Program
	{
        [STAThread]
        public static void Main(string[] args)
		{
			Neymana_Dirihle problem = new Neymana_Dirihle(GetH(), GetF1_1());

			var destiny = problem.CalculateDestiny();
			var resNabl = new decimal[2 * Parameters.M];
			var resExact = new decimal[2 * Parameters.M];
			for (int i = 0; i < resNabl.Length; i++)
			{
				var t = i * (decimal) Math.PI / Parameters.M;
				var points = GetPoint(t);
				resNabl[i] = problem.CalculateU(destiny, points[0], points[1]);
				resExact[i] = GetF(points[0], points[1]);
			}

            Form1 form = new Form1();
            //form.Draw1(destiny,problem.InitTVector());
            form.Draw1(resNabl,problem.InitTVector());
            form.Draw2(resExact,problem.InitTVector());
            form.Show();
                
            Application.EnableVisualStyles();
            Application.Run(form);

            //Console.WriteLine(Math.Abs(nablRes-exactRes));
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

				res[i] = dod1 + dod2;
			}

			return res;
		}

		private static decimal[] GetF1_1()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				res[i] = (decimal)(Math.Pow((double)Parametrization_ND.X11(t),2)-Math.Pow((double)Parametrization_ND.X12(t),2));
			}

			return res;
		}
		
		private static decimal[] GetF1_0()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				res[i] = (decimal)(Math.Pow((double)Parametrization_ND.X01(t),2)-Math.Pow((double)Parametrization_ND.X02(t),2));
			}

			return res;
		}
		
		private static decimal GetF(decimal x1, decimal x2)
		{
			return x1 * x1 - x2 * x2;
		}
		
		private static decimal[] GetF()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				
				res[i] = (decimal)(Math.Pow(3.0*Math.Cos((double)t),2)-Math.Pow(3.0*Math.Sin((double)t),2));
			}

			return res;
		}
		
		private static decimal[] GetPoint(decimal t)
		{
			var x1 = (decimal) (3.0 * Math.Cos((double) t));
			var x2 = (decimal) (3.0 * Math.Sin((double) t));
			if (x1 != x2)
			{
				return new[] {x1, x2};
			}
			return new[] {x1, x2+0.001M};
		}
	}
}