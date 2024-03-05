using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models
{
	public class Item
	{
		public Item() { }
		public Item(string Image)
		{
			this.image = Image;
		}
		[Key] public int ID { get; set; }
		[Required] public string name { get; set; } = "Unnamed";
		[Required] public bool recyclable { get; set; } = false;
		[Required] public int capacity { get; set; } = 1;
		[Required] public float value { get; set; } = 5.00f;
		[Required] public string image { get; set; } = ""; // image file location
		[Required][Range(1,3)] public int rarity { get; set; } = 1;

		public virtual void OnRecycle(PlayerData data)
		{
			if(recyclable)data.CollectItem(this);
			else data.CollectBadItem(this);
		}

		public virtual void OnTrash(PlayerData data)
		{
			
		}
	}
}
