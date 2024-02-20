using ExtremeRecycler.Data.DALs;
using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtremeRecycler.Controllers
{
    public class GameController : Controller
    {
		DataAccessLayer<Item> ItemDal;
		DataAccessLayer<Upgrade> UpgradeDal;
		DataAccessLayer<PlayerData> PlayerDal;
		public GameController(DataAccessLayer<Item> indalItem, DataAccessLayer<Upgrade> indalUpgrade, DataAccessLayer<PlayerData> indalPlayer)
		{
			ItemDal = indalItem;
			UpgradeDal = indalUpgrade;
			PlayerDal = indalPlayer;
		}
		public IActionResult Trash(BigModel model)
        {
            model.Item.OnTrash();
            return View();
        }

        public IActionResult Recycle(BigModel model)
        {
            model.Item.OnRecycle();
            return View();
        }

        public IActionResult BuyUpgrade(BigModel model)
        {
            return View();
        }

        public IActionResult Sell(BigModel model)
        {
            model.playerData.Dollars += model.playerData.bin.totalValue;
            model.playerData.bin.EmptyBin();
            return View();
        }

        public IActionResult Leaderboard()
        {
            return View(PlayerDal.GetAll());
            //return View();
        }
    }
}
