using ExtremeRecycler.Data.DALs;
using ExtremeRecycler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtremeRecycler.Controllers
{
    public class GameController : Controller
    {
        PlayerDataList playerDataDAL = new PlayerDataList();
        UpgradeDataList upgradeDataDAL = new UpgradeDataList();
        public IActionResult Trash(PlayerData pd, Item item)
        {
            return View();
        }

        public IActionResult Recycle(PlayerData pd, Item item)
        {
            return View();
        }

        public IActionResult BuyUpgrade(PlayerData pd)
        {
            return View();
        }

        public IActionResult Sell(PlayerData pd)
        {
            return View();
        }

        public IActionResult Leaderboard()
        {
            //return View(playerDataDAL.GetAll());
            return View();
        }
    }
}
