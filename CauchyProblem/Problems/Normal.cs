using System;

namespace CauchyProblem.Problems
{
	public class Normal
	{
		public static decimal[] GetNormal(decimal t, Func<decimal, decimal> x1d,
			Func<decimal, decimal> x2d)
		{
			return new [] {x2d(t) / GetModul(t, x1d, x2d), -x1d(t) / GetModul(t, x1d, x2d)};
		}

		private static decimal GetModul(decimal t, Func<decimal, decimal> x1d,
			Func<decimal, decimal> x2d)
		{
			return (decimal)Math.Sqrt(Math.Pow((double) Math.Abs(x1d(t)), 2) + Math.Pow((double) Math.Abs(x2d(t)), 2));
		}
		
		public static decimal GetModul(decimal[] x)
		{
			return (decimal)Math.Sqrt(Math.Pow((double)x[0], 2) + Math.Pow((double)x[1], 2));
		}

		public static decimal R(decimal tj, decimal t)
		{
			decimal sum = 0;
			for (int m = 1; m < Parameters.M ; m++)
			{
				sum+=(decimal)Math.Cos((double)((decimal)m*(t-tj)))/(decimal)m;
			}

			return -1M / (2M * Parameters.M) *
			       (1M + 
			        2M * sum + 
			        (decimal) Math.Cos((double) (t - tj)) / (decimal)Parameters.M);
		}
	}
}