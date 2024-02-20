using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using System;

namespace ExtremeRecycler.Data.DALs
{
	public class UpgradeDataList : DataAccessLayer<ValueUpgrade>
	{
		private ApplicationDbContext db;
		public UpgradeDataList(ApplicationDbContext indb)
		{
			db = indb;
		}

		public bool Add(ValueUpgrade item)
		{
			db.Add(item);
			db.SaveChanges();
			return true;
		}

		public ValueUpgrade Get(int id)
		{
			return db.Upgrades.FirstOrDefault(x => x.ID == id);
		}

		public List<ValueUpgrade> GetAll()
		{
			return db.Upgrades.ToList();
		}

		public bool Remove(int id)
		{
			ValueUpgrade data = db.Upgrades.FirstOrDefault(x => x.ID == id);
			db.Remove(data);
			db.SaveChanges();
			return true;
		}

		public void Update(ValueUpgrade item)
		{
			db.Update(item);
			db.SaveChanges();
		}
	}
}
