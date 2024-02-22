using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using System;

namespace ExtremeRecycler.Data.DALs
{
	public class PlayerUpgradeDataList : DataAccessLayer<PlayerUpgrade>
	{
		private ApplicationDbContext db;
		public PlayerUpgradeDataList(ApplicationDbContext indb)
		{
			db = indb;
		}

		public bool Add(PlayerUpgrade item)
		{
			db.Add(item);
			db.SaveChanges();
			return true;
		}

		public PlayerUpgrade Get(int id)
		{
			return db.PlayerUpgrades.FirstOrDefault(x => x.ID == id);
		}

		public List<PlayerUpgrade> GetAll()
		{
			return db.PlayerUpgrades.ToList();
		}

		public bool Remove(int id)
		{
			PlayerUpgrade data = db.PlayerUpgrades.FirstOrDefault(x => x.ID == id);
			db.Remove(data);
			db.SaveChanges();
			return true;
		}

		public void Update(PlayerUpgrade item)
		{
			db.Update(item);
			db.SaveChanges();
		}
	}
}
