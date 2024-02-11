namespace ExtremeRecycler.Models
{
	public class Bin
	{
		public int maxCapacity = 100;
		public int currentCapacity = 0;

		// item contributes to totalValue when recycled
		public float totalValue = 0;
		

		public void EmptyBin()
		{
			currentCapacity = 0;
			totalValue = 0;
		}
	}
}
