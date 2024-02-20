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
            return View(playerDataDAL.GetAll());
            return View();
        }
    }
}
