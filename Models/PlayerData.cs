using ExtremeRecycler.Models.Upgrades;

namespace ExtremeRecycler.Models
{
    public class PlayerData
	{
		public PlayerData() { }
		public PlayerData(float dollars)
		{
			this.Dollars = dollars;
		}
		public float Dollars { get; set; } = 0.00f;
		private List<Upgrade> Upgrades = new List<Upgrade>();
		public Bin bin = new Bin();
<<<<<<< Updated upstream
=======

		public void AddUpgrade(Upgrade upgrade)
		{
			AcquiredUpgrades.Add(upgrade);
		}
		public string Username { get; set; }
>>>>>>> Stashed changes
	}
}
