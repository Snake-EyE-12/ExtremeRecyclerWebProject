using ExtremeRecycler.Data.DALs;
using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
        private BigModel GetNewPageData()
        {

            PlayerData currentPlayerData = GetMatchingPlayerData();
            Item item = GetRandomItem();
            return new BigModel(currentPlayerData, item);
        }
        private Item GetRandomItem()
        {
            Random random = new Random();
            //Upgrade Randomization
            return ItemDal.GetAll()[random.Next() % ItemDal.GetAll().Count];
        }
        private PlayerData GetMatchingPlayerData()
        {
			string currentPlayer = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(currentPlayer == null)
            {
                Console.WriteLine("Not Signed In");
                //Return Error or Login Page
            }
			PlayerData data = PlayerDal.GetAll().FirstOrDefault(x => x.Username.Equals(currentPlayer));
            if(data != default(PlayerData))
            {
                return data;
            }
            ValueUpgrade[] newUpgrades = new ValueUpgrade[UpgradeDal.GetAll().Count];
            UpgradeDal.GetAll().CopyTo(newUpgrades.ToArray());

			PlayerData pd = new PlayerData(0, newUpgrades.ToList(), currentPlayer);
            PlayerDal.Add(pd);
            return pd;
		}
		public IActionResult Trash(BigModel model)
        {
			return RedirectToAction("GamePage", "Game", GetNewPageData());
			model.Item.OnTrash();
            return View();
        }

        public IActionResult Recycle(Item item)
        {
            PlayerData pd = GetMatchingPlayerData();
            item.OnRecycle(pd);
            PlayerDal.Update(pd);
            return RedirectToAction("GamePage", "Game", GetNewPageData());
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
		public IActionResult GamePage()
		{
			return View(GetNewPageData());
		}
		public IActionResult TempUpgradePage()
		{
            return View(UpgradeDal.GetAll());
            //return View();
		}
	}
}
