using System;

namespace CauchyProblem.Problems.Parametrization
{
	public class Parametrization_ND
	{
		private static decimal R1 = 4;// TODO jump in graph depents on this value
		private static decimal R0 = 2;

		public static decimal[] X0(decimal t)
		{
			return new [] {X01(t),X02(t) };
		}
		
		public static decimal[] X1(decimal t)
		{
			return new [] {X11(t),X12(t) };
		}
		
		public static decimal X01(decimal t)//Г0
		{
			return R0*(decimal)Math.Cos((double)t);
		}
		
		public static decimal X02(decimal t)//Г0
		{
			return R0*(decimal)Math.Sin((double)t);
		}
		
		public static decimal X11(decimal t)//Г1
		{
			return R1*(decimal)Math.Cos((double)t);
		}
		
		public static decimal X12(decimal t)//Г1
		{
			return R1*(decimal)Math.Sin((double)t);
		}
		
		//diferect
		public static decimal X01d(decimal t)//Г0
		{
			return -R0*(decimal)Math.Sin((double)t);
		}
		
		public static decimal X02d(decimal t)//Г0
		{
			return R0*(decimal)Math.Cos((double)t);
		}
		
		public static decimal X11d(decimal t)//Г1
		{
			return -R1*(decimal)Math.Sin((double)t);
		}
		
		public static decimal X12d(decimal t)//Г1
		{
			return R1*(decimal)Math.Cos((double)t);
		}
		
		//differect 2 
		public static decimal X01d2(decimal t)//Г0
		{
			return -R0*(decimal)Math.Cos((double)t);
		}
		
		public static decimal X02d2(decimal t)//Г0
		{
			return -R0*(decimal)Math.Sin((double)t);
		}
		
		public static decimal X11d2(decimal t)//Г1
		{
			return -R1*(decimal)Math.Cos((double)t);
		}
		
		public static decimal X12d2(decimal t)//Г1
		{
			return -R1*(decimal)Math.Sin((double)t);
		}
	}
}