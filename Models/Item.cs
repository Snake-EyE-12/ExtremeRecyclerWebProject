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
		[Key] public int id { get; set; }
		[Required] public string name { get; set; } = "Unnamed";
		[Required] public bool recyclable { get; set; } = false;
		[Required] public int capacity { get; set; } = 1;
		[Required] public float value { get; set; } = 5.00f;
		[Required] public string image { get; set; } = ""; // image file location

		public virtual void OnLeftClick()
		{
			Recycle();
		}

		public virtual void OnRightClick()
		{
			Trash();
		}

		protected void Recycle()
		{

		}

		protected void Trash()
		{

		}
	}
}
