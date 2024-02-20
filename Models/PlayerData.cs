using ExtremeRecycler.Models.Upgrades;
using System.ComponentModel.DataAnnotations;


namespace ExtremeRecycler.Models
{
	public class PlayerData
	{
		public PlayerData() { }
		public PlayerData(float dollars)
		{
			this.Dollars = dollars;
		}
		[Key] public int ID { get; set; }
		[Required] public float Dollars { get; set; } = 0.00f;
		private List<Upgrade> Upgrades = new List<Upgrade>();
		[Required] public Bin bin = new Bin();
		[Required] public string Username { get; set; }

		public void AddUpgrade(Upgrade upgrade)
		{
			Upgrades.Add(upgrade);
		}

	}
}
