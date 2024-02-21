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
		DataAccessLayer<ValueUpgrade> UpgradeDal;
		DataAccessLayer<PlayerData> PlayerDal;
		public GameController(DataAccessLayer<Item> indalItem, DataAccessLayer<ValueUpgrade> indalUpgrade, DataAccessLayer<PlayerData> indalPlayer)
		{
			ItemDal = indalItem;
			UpgradeDal = indalUpgrade;
			PlayerDal = indalPlayer;
		}
		public IActionResult Trash(Item item)
        {
            item.OnTrash();
            return View();
        }

		public IActionResult Recycle()
		{
			return View("Index");
		}

		public IActionResult Recycle(Item item)
        {
            item.OnRecycle();
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
        }
		public IActionResult TempUpgradePage()
		{
            return View(UpgradeDal.GetAll());
            //return View();
		}
	}
}
