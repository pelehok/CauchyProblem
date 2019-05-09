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

		public static decimal GetModul(decimal t, Func<decimal, decimal> x1d,
			Func<decimal, decimal> x2d)
		{
			return (decimal)Math.Sqrt(Math.Pow((double) Math.Abs(x1d(t)), 2) + Math.Pow((double) Math.Abs(x2d(t)), 2));
		}
		
		public static decimal GetModulWithQuare(decimal t, Func<decimal, decimal> x1d,
			Func<decimal, decimal> x2d)
		{
			return (decimal)(Math.Pow((double) Math.Abs(x1d(t)), 2) + Math.Pow((double) Math.Abs(x2d(t)), 2));
		}
	}
}