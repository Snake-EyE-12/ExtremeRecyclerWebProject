using ExtremeRecycler.Models.Upgrades;
using System.ComponentModel.DataAnnotations;


namespace ExtremeRecycler.Models
{
	public class PlayerData
	{
		public PlayerData() { }
		public PlayerData(float dollars, string name, string displayName)
		{
			this.Dollars = dollars;
			this.Username = name;
			DisplayName = displayName;
		}
		[Key] public int ID { get; set; }
		[Required] public string Username { get; set; }
		[Required] public string DisplayName { get; set; }
		[Required] public float Dollars { get; set; } = 0.00f;
		[Required] public float binMaxCapacity { get; set; } = 100.0f;
		[Required] public float binCurrentCapacity { get; set; } = 0.0f;
		[Required] public float binValue { get; set; } = 0.0f;
		[Required] public DateTime sellAvailableTime { get; set; }

		public float binBaseMaxCapacity = 100;

        public void EmptyBin()
        {
            binCurrentCapacity = 0;
            binValue = 0;
        }

        public void CollectItem(Item item)
        {
            binCurrentCapacity += item.capacity;
            binValue += item.value;
        }
    }
}
