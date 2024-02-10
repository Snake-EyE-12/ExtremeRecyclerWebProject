namespace ExtremeRecycler.Models
{
	public class Item
	{
		public int id;
		public string name = "Unnamed";
		public bool recyclable = false;
		public int capacity = 1;
		public int value = 5;
		public string image = ""; // image file location

		public virtual void OnLeftClick()
		{
			Recycle();
		}

		public virtual void OnRightClick()
		{
			Trash();
		}

		private void Recycle()
		{

		}

		private void Trash()
		{

		}
	}
}
