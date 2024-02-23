﻿using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models
{
	public class Item
	{
		public delegate void RecycleEventHandler(object sender, RecycleEventArgs e);

		public event RecycleEventHandler RecycleEvent;

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
		[Required][Range(1,5)] public int rarity { get; set; } = 1;

		public virtual void OnRecycle(PlayerData data)
		{
			data.CollectItem(this);
			//Recycle();
		}

		public virtual void OnTrash(PlayerData data)
		{

		}

		protected void Recycle()
		{
			OnRecycle(new RecycleEventArgs(value, capacity));
		}

		protected virtual void OnRecycle(RecycleEventArgs e)
		{
			RecycleEventHandler handler = RecycleEvent;
			handler?.Invoke(this, e);
		}

		protected void Trash()
		{

		}
	}

	public class RecycleEventArgs : EventArgs
	{
		public float value { get; }
		public int capacity { get; }

		public RecycleEventArgs(float value, int capacity)
		{
			this.value = value;
			this.capacity = capacity;
		}
	}
}
