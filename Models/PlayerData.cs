using ExtremeRecycler.Models.Upgrades;
using System.ComponentModel.DataAnnotations;


namespace ExtremeRecycler.Models
{
	public class PlayerData
	{
		public PlayerData() { }
		public PlayerData(float dollars, List<ValueUpgrade> upgrades, string name)
		{
			this.Dollars = dollars;
			this.Upgrades = upgrades;
			this.Username = name;
		}
		[Key] public int ID { get; set; }
		[Required] public float Dollars { get; set; } = 0.00f;
		private List<ValueUpgrade> Upgrades = new List<ValueUpgrade>();
		[Required] public Bin bin = new Bin();
		[Required] public string Username { get; set; }

		public void AddUpgrade(ValueUpgrade upgrade)
		{
			Upgrades.Add(upgrade);
		}

	}
}
