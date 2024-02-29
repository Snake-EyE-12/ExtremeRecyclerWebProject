using ExtremeRecycler.Data.DALs;
using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;
using System.Security.Claims;

namespace ExtremeRecycler.Controllers
{
    public class GameController : Controller
    {
		DataAccessLayer<Item> ItemDal;
		DataAccessLayer<ValueUpgrade> UpgradeDal;
		DataAccessLayer<PlayerData> PlayerDal;
        DataAccessLayer<PlayerUpgrade> PlayerUpgradeDal;
		public GameController(DataAccessLayer<Item> indalItem, DataAccessLayer<ValueUpgrade> indalUpgrade, DataAccessLayer<PlayerData> indalPlayer, DataAccessLayer<PlayerUpgrade> indalPlayerUpgrade)
		{
			ItemDal = indalItem;
			UpgradeDal = indalUpgrade;
			PlayerDal = indalPlayer;
            PlayerUpgradeDal = indalPlayerUpgrade;
		}
        private BigModel GetNewPageData()
        {
            PlayerData currentPlayerData = GetMatchingPlayerData();
            Item item = GetRandomItem();
            IEnumerable<ValueUpgrade> playersUpgrades = GetPlayerUpgrades(currentPlayerData.Username);
            return new BigModel(currentPlayerData, item, playersUpgrades);
        }
        private IEnumerable<ValueUpgrade> GetPlayerUpgrades(string associatedUser)
        {
			List<ValueUpgrade> result = new List<ValueUpgrade>();
            IEnumerable<PlayerUpgrade> playersUpgrades = PlayerUpgradeDal.GetAll().Where(x => x.AssociatedUser.Equals(associatedUser));
            foreach (var item in playersUpgrades)
            {
                ValueUpgrade respectiveUpgrade = UpgradeDal.Get(item.UpgradeID);
                result.Add(new ValueUpgrade(respectiveUpgrade.ID, respectiveUpgrade.UpgradeName, respectiveUpgrade.DisplayName, respectiveUpgrade.Description, respectiveUpgrade.BackgroundImage, respectiveUpgrade.BaseCost, respectiveUpgrade.CostScalar, respectiveUpgrade.BaseValue, respectiveUpgrade.ValueScalar, item.UpgradeLevel));
            }
            return result;
        }
        private Item GetRandomItem()
        {
            Random random = new Random();
			//Upgrade Randomization
			// ===================================================================================Upgrade Check - Item Rarity
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
            

            //PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 1, 0));
            PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 2, 0));
            //PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 3, 0)); //FOR CHASE - THIS WAS THE PENALTY ONE
            PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 10, 0)); //FOR JACOB - THIS WAS THE SELL MULTIPLIER
            //ADD ALL UPGRADES WITH ASSOCIATED ID

			PlayerData pd = new PlayerData(0, currentPlayer);
            PlayerDal.Add(pd);
            return pd;
		}
		public IActionResult Trash(int itemID)
        {
			return RedirectToAction("GamePage", "Game", GetNewPageData());
            // CURRENTLY DOES NOTHING BUT A PAGE REFRESH ???
        }

        public IActionResult Recycle(int itemID)
        {
            PlayerData pd = GetMatchingPlayerData();
            Item item = ItemDal.Get(itemID);
            if(item.recyclable)
            {
                item.OnRecycle(pd);
            }
            else pd.Dollars -= GetUpgradeValue("PenaltyMinimizer", pd); // CAN GO BELOW ZERO
			PlayerDal.Update(pd);
            return RedirectToAction("GamePage", "Game", GetNewPageData());
        }


        public IActionResult BuyUpgrade(string upgradeName) //SHOULD WORK WITH ALL UPGRADES IF ADDED PROPERLY
        {
            PlayerData pd = GetMatchingPlayerData();
			IEnumerable<ValueUpgrade> upgrades = GetPlayerUpgrades(pd.Username);
			foreach (ValueUpgrade upgrade in upgrades)
			{
				if (upgrade.UpgradeName == upgradeName)
				{
					if(upgrade.AttemptPurchase(pd))
                    {
                        PlayerDal.Update(pd);
                        UpdatePlayerUpgrade(pd, upgrade);
                        break;
                    }

                    
				}
			}
			return RedirectToAction("GamePage", "Game", GetNewPageData());
		}

        private void UpdatePlayerUpgrade(PlayerData pd, ValueUpgrade upgrade)
        {
            var pUpgrade = PlayerUpgradeDal.GetAll().Where(x => x.UpgradeID == upgrade.matchingID);
            var sUpgrade = pUpgrade.First(x => x.AssociatedUser == pd.Username);
			sUpgrade.UpgradeLevel++;
            PlayerUpgradeDal.Update(sUpgrade);
            if (upgrade.UpgradeName.Equals("BinCapacity"))
            {
                pd.binMaxCapacity = pd.binBaseMaxCapacity + GetUpgradeValue("BinCapacity", pd);
            }
		}

        public IActionResult Sell(int id)
        {
			// need to check timer at this point
			// ===================================================================================Upgrade Check - Truck Delay
			//if true (timer has past) then start another here
			PlayerData playerData = PlayerDal.Get(id);
			playerData.Dollars += playerData.binValue * GetUpgradeValue("SellMultiplier", playerData);
            playerData.EmptyBin();
            PlayerDal.Update(playerData);
			return RedirectToAction("GamePage", "Game", GetNewPageData());
		}

        public IActionResult Leaderboard()
        {
            // sort algorythm goes here
            ViewBag.name = User.FindFirstValue(ClaimTypes.NameIdentifier);
			List<PlayerData> players = PlayerDal.GetAll();
            var sortedList = players.OrderByDescending(obj => obj.Dollars).ToList();
            return View(sortedList);
        }

		public IActionResult GamePage()
		{
			return View(GetNewPageData());
		}

		public IActionResult TempUpgradePage() //CAN BE REMOVED
		{
            return View(UpgradeDal.GetAll());
		}

        private float GetUpgradeValue(string upgradeName, PlayerData player)
        {
            IEnumerable<ValueUpgrade> upgrades = GetPlayerUpgrades(player.Username);
            foreach (ValueUpgrade upgrade in upgrades)
            {
                if(upgrade.UpgradeName == upgradeName)
                {
                    return upgrade.Execute();
                }
            }
            //If it gets here, there was a Problem
            return 0.0f;
        }
	}
}
