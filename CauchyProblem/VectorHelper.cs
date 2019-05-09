using System;

namespace CauchyProblem
{
	public class VectorHelper
	{
		public static decimal[] Sum(decimal[] v1, decimal[] v2)
		{
			var res = new decimal[v1.Length];

			for (int i = 0; i < v1.Length; i++)
			{
				res[i] = v1[i] + v2[i];
			}

			return res;
		}
		
		public static decimal[] Div(decimal[] v1, decimal[] v2)
		{
			var res = new decimal[v1.Length];

			for (int i = 0; i < v1.Length; i++)
			{
				res[i] = v1[i] - v2[i];
			}

			return res;
		}
	}
}