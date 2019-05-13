namespace CauchyProblem
{
	public class CauchyProblem_InputData
	{
		public static decimal GetF1(decimal x1, decimal x2)//u Г0
		{
			return x1 * x1 + x2 * x2;
		}
		
		public static decimal GetF2(decimal x1, decimal x2)//du Г0
		{
			return x1 * x1 + x2 * x2;
		}
	}
}