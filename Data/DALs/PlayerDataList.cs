using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using System;

namespace ExtremeRecycler.Data.DALs
{
    public class PlayerDataList : DataAccessLayer<PlayerData>
    {
		private ApplicationDbContext db;
		public PlayerDataList(ApplicationDbContext indb)
		{
			db = indb;
		}

		public bool Add(PlayerData item)
        {
            db.Add(item);
            db.SaveChanges();
            return true;
        }

        public PlayerData Get(int id)
        {
            return db.PlayerData.FirstOrDefault(x => x.ID == id);
        }

        public List<PlayerData> GetAll()
        {
            return db.PlayerData.ToList();
        }

        public bool Remove(int id)
        {
			PlayerData data = db.PlayerData.FirstOrDefault(x => x.ID == id);
            db.Remove(data);
			db.SaveChanges();
			return true;
		}

        public void Update(PlayerData item)
        {
            db.Update(item);
            db.SaveChanges();
        }
    }
}
