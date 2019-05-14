using System;
using CauchyProblem.Problems.Parametrization;

namespace CauchyProblem.Problems.Kernels
{
	public class Kernels_ND
	{
		public static decimal Fundamental(decimal[] x1, decimal[] x2)
		{
			return (decimal)Math.Log((double)(1M / Normal.GetModul(
				                                  VectorHelper.Div(
					                                  x1,
					                                  x2))));
		}
		
		public static decimal K01(decimal t, decimal tay)
		{
			if (t != tay)
			{
				return VectorHelper.Mult(
					       VectorHelper.Div(
						       Parametrization_ND.X0(tay),
						       Parametrization_ND.X0(t)),
					       Normal.GetNormal(t,
						       Parametrization_ND.X01d,
						       Parametrization_ND.X02d))
				       / (decimal) Math.Pow(
					       (double) Normal.GetModul(
									VectorHelper.Div(
											Parametrization_ND.X0(t),
											Parametrization_ND.X0(tay)))
									, 2);
			}
			else
			{
				return VectorHelper.Mult(
					       new []{Parametrization_ND.X01d2(t),Parametrization_ND.X02d2(t)},
					       Normal.GetNormal(t,
						       Parametrization_ND.X01d,
						       Parametrization_ND.X02d))
				       / (2M * (decimal)Math.Pow(
					        (double)Normal.GetModul(new []
					        {
						        Parametrization_ND.X01d(t),
						        Parametrization_ND.X02d(t)
					        })
					        , 2));
			}
		}

		public static decimal K02(decimal t, decimal tay)
		{
			return VectorHelper.Mult(
				       VectorHelper.Div(
					       Parametrization_ND.X1(tay),
					       Parametrization_ND.X0(t)),
				       Normal.GetNormal(t,
					       Parametrization_ND.X01d,
					       Parametrization_ND.X02d))
			       / (decimal) Math.Pow(
				       (double) Normal.GetModul(
					       VectorHelper.Div(
						       Parametrization_ND.X0(t),
						       Parametrization_ND.X1(tay)))
				       , 2);
		}

		public static decimal P11(decimal t, decimal tay)
		{
			return (decimal)Math.Log(
				(double)(1M/(
					         Normal.GetModul(
						         VectorHelper.Div(
							         Parametrization_ND.X1(t),
							         Parametrization_ND.X0(tay))))));
		}
		
		public static decimal P12(decimal t, decimal tay)
		{
			return P12_1(t, tay) + P12_2(t, tay);
		}

		public static decimal P12_1(decimal t, decimal tay)
		{
			return -0.5M * (decimal) Math.Log(
				       4 / Math.E *
				       Math.Pow(
					       Math.Sin(
						       (double) (t - tay) / 2
					       ), 2)
			       );
		}
		
		public static decimal P12_2(decimal t, decimal tay)
		{
			if (t != tay)
			{
				return (decimal) Math.Log(4 / Math.E *
				                          Math.Pow(
					                          Math.Sin(
						                          (double) (t - tay) / 2
					                          ), 2)
				                          /Math.Pow((double)Normal.GetModul(
					                          VectorHelper.Div(
						                          Parametrization_ND.X1(t),
						                          Parametrization_ND.X1(tay))),2))/2;
			}
			else
			{
				return (decimal) Math.Log(1 /
				                          (Math.E * Math.Pow((double)
					                           Normal.GetModul(new[]
					                           {
						                           Parametrization_ND.X11d(t),
						                           Parametrization_ND.X12d(t)
					                           })
					                           , 2)))/2;
			}
		}
	}
}