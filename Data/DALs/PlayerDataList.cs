using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using System;

namespace ExtremeRecycler.Data.DALs
{
    public class PlayerDataList : DataAccessLayer<PlayerData>
    {
        //private AppDbContext db;
        //public UpgradeDataList(AppDbContext indb)
        //{
        //	db = indb;
        //}
        public bool Add(PlayerData item)
        {
            throw new NotImplementedException();
        }

        public PlayerData Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<PlayerData> GetAll()
        {
            throw new NotImplementedException();
            //return db.PlayerDatas;
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PlayerData item)
        {
            throw new NotImplementedException();
        }
    }
}
