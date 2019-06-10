using System;
using System.Linq;
using System.Windows.Forms;
using CauchyProblem.Problems;
using CauchyProblem.Problems.Parametrization;
using Charts;

namespace CauchyProblem
{
	internal class Program
	{
		private static readonly bool UseNoisy = true;
        [STAThread]
        public static void Main(string[] args)
		{
			Alter alt = new Alter();
			var t = alt.Procces(UseNoisy);
			
			var errorU = t.Item1;
			var errorDU = t.Item2;

			
            Form1 form = new Form1();

            var index =  Enumerable.Range(1, 50).Select(x=>(decimal)x).ToArray();
			form.Draw1(errorU.ToList().GetRange(1,errorU.Length-2).ToArray(),index);
            form.Draw2(errorDU.ToList().GetRange(1,errorU.Length-2).ToArray(),index);
            form.Show();

            //Console.WriteLine(Error(resExact,resNabl));
                
            Application.EnableVisualStyles();
            Application.Run(form);
		}

        private static decimal[] Error(decimal[] ex, decimal[] toch)
        {
	        var res = new decimal[2*Parameters.M];

	        for (int i = 0; i < 2*Parameters.M; i++)
	        {
		        res[i] = Math.Abs(ex[i] - toch[i]);
	        }

	        return res;
        }
        
		private static decimal[] GetH_0()
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
		
		private static decimal[] GetH_1()
		{
			var res = new decimal[2 * Parameters.M];


			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal) ((decimal) i * (decimal) Math.PI) / (decimal) Parameters.M;
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

				res[i] = (dod1 + dod2);
			}

			return res;
		}

		private static decimal[] GetRozbitta()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				var t = (decimal)((decimal)i * (decimal)Math.PI) / (decimal)Parameters.M;
				res[i] = t;
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
			var x1 = (decimal) (0.9 * Math.Cos((double) t));
			var x2 = (decimal) (0.9 * Math.Sin((double) t));
			if (x1 != x2)
			{
				return new[] {x1, x2};
			}
			return new[] {x1, x2};
		}
		
		private static decimal[] GetH()
		{
			var res = new decimal[2 * Parameters.M];
			for (int i = 0; i < res.Length; i++)
			{
				res[i] = 0M;
			}

			return res;
		}
	}
}