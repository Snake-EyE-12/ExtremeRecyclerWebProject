using ExtremeRecycler.Models.Upgrades;

namespace ExtremeRecycler.Models
{
    public class PlayerData
	{
		public float Dollars = 0.00f;
		private List<Upgrade> AcquiredUpgrades = new List<Upgrade>();
		public Bin bin = new Bin();

		public void AddUpgrade(Upgrade upgrade)
		{
			AcquiredUpgrades.Add(upgrade);
		}
	}
}
