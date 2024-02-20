using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using System;

namespace ExtremeRecycler.Data.DALs
{
	public class ItemDataList : DataAccessLayer<Item>
	{
		private ApplicationDbContext db;
		public ItemDataList(ApplicationDbContext indb)
		{
			db = indb;
		}

		public bool Add(Item item)
		{
			db.Items.Add(item);
			db.SaveChanges();
			return true;
		}

		public Item Get(int id)
		{
			return db.Items.FirstOrDefault(x => x.ID == id);
		}

		public List<Item> GetAll()
		{
			return db.Items.ToList();
		}

		public bool Remove(int id)
		{
			Item data = db.Items.FirstOrDefault(x => x.ID == id);
			db.Remove(data);
			db.SaveChanges();
			return true;
		}

		public void Update(Item item)
		{
			db.Update(item);
			db.SaveChanges();
		}
	}
}
