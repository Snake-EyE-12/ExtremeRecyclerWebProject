namespace ExtremeRecycler.Models
{
	public class PlayerData
	{
		public float Dollars = 0.00f;
		private List<Upgrade> Upgrades = new List<Upgrade>();

		public void AddUpgrade(Upgrade upgrade)
		{
			Upgrades.Add(upgrade);
		}
	}
}
