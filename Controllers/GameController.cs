using ExtremeRecycler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExtremeRecycler.Controllers
{
    public class GameController : Controller
    {
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
    }
}
