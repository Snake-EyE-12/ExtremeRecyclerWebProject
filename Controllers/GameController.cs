using ExtremeRecycler.Data.DALs;
using ExtremeRecycler.Interfaces;
using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        static DateTime startTime;

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
			ViewBag.name = User.FindFirstValue(ClaimTypes.NameIdentifier);
			IEnumerable<PlayerData> allplayerdata = PlayerDal.GetAll().OrderByDescending(obj => obj.Dollars);
            return new BigModel(currentPlayerData, item, playersUpgrades, allplayerdata);
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
            PlayerData pd = GetMatchingPlayerData();
            float randomValue = random.Next(1, 1000);
            randomValue += GetUpgradeValue("ItemRarity", pd);

            IEnumerable<Item> itemList;
            if(randomValue > 980)
            {
                itemList = ItemDal.GetAll().Where(x => x.rarity == 3);
            }
            else if(randomValue > 900)
            {
                itemList = ItemDal.GetAll().Where(x => x.rarity == 2);
            }
            else
            {
                itemList = ItemDal.GetAll().Where(x => x.rarity == 1);
            }

            return itemList.ElementAt(random.Next() % itemList.Count());
        }
        private PlayerData GetMatchingPlayerData()
        {
			string currentPlayer = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(currentPlayer == null)
            {
                Console.WriteLine("Not Signed In");
                //Return Error or Login Page
                return null;
            }
			PlayerData data = PlayerDal.GetAll().FirstOrDefault(x => x.Username.Equals(currentPlayer));
            if(data != default(PlayerData))
            {
                return data;
            }
            

            PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 2, 0));
            PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 6, 0));
            PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 9, 0)); 
            PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 10, 0));
            PlayerUpgradeDal.Add(new PlayerUpgrade(currentPlayer, 11, 0)); 

			PlayerData pd = new PlayerData(0, currentPlayer, "Enter Your Name");
            PlayerDal.Add(pd);
            return pd;
		}
		public IActionResult Trash(int itemID)
        {
			Item item = ItemDal.Get(itemID);

            if(item.recyclable)
            {
			    ViewBag.textColor = "danger";
            }
            else
            {
                ViewBag.textColor = "success";
            }

			ViewBag.headerSize = 1;
			ViewBag.feedbackText = item.name + " Trashed!!";

			return View("GamePage", GetNewPageData());
            // CURRENTLY DOES NOTHING BUT A PAGE REFRESH ???
        }

        public IActionResult Recycle(int itemID)
        {
			PlayerData pd = GetMatchingPlayerData();
            Item item = ItemDal.Get(itemID);



            if(pd.binMaxCapacity - pd.binCurrentCapacity > item.capacity)
            {
				item.OnRecycle(pd);
                if(!item.recyclable)
                {
                    pd.Dollars -= item.value * GetUpgradeValue("PenaltyMinimizer", pd); // CAN GO BELOW ZERO
				    ViewBag.textColor = "danger";
                }
                else
                {
                    ViewBag.textColor = "success";
                }
                    
				ViewBag.headerSize = 1;
				ViewBag.feedbackText = item.name + " Recycled!!";
            }
            else
            {
				ViewBag.headerSize = 1;
				ViewBag.textColor = "danger";
				ViewBag.feedbackText = "Bin was too full for " + item.name + "!";
			}
            
			PlayerDal.Update(pd);
            return View("GamePage", GetNewPageData());
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
						ViewBag.headerSize = 2;
						ViewBag.textColor = "primary";
                        ViewBag.feedbackText = upgrade.UpgradeName + " Upgrade Bought!";

						PlayerDal.Update(pd);
                        UpdatePlayerUpgrade(pd, upgrade);
                        break;
                    }
                    else
                    {
						ViewBag.headerSize = 2;
						ViewBag.textColor = "danger";
						ViewBag.feedbackText = upgrade.UpgradeName + " is too expensive!";
					}
				}
			}
			return View("GamePage", GetNewPageData());
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
            PlayerDal.Update(pd);
		}

        [Authorize]
        public IActionResult ProgressBar()
        {
            //ViewBag.Date = GetMatchingPlayerData().sellAvailableTime.Second.ToString(); //debug when the bar should be filled at the seconds place

            //PlayerData playerData = PlayerDal.Get(id);
            if (GetMatchingPlayerData().sellAvailableTime.CompareTo(DateTime.Now) > 0)
            {
				float updatedData = getTimePassed() / GetUpgradeValue("TruckDelay", GetMatchingPlayerData());

				//currentSecond += 0.11f; //runs ever .1 seconds but a little extra to accomidate for lag and page refresh
				updatedData *= 100;

				return PartialView("partialBar", updatedData);
			}

			return PartialView("partialBar", 0f);
		}

        private float getTimePassed()
        {
            float miliseconds = DateTime.Now.Millisecond - startTime.Millisecond;
			float seconds = DateTime.Now.Second - startTime.Second;
			float minutes = DateTime.Now.Minute - startTime.Minute;
            float hours = DateTime.Now.Hour - startTime.Hour;

			return ((hours * 3600) + (minutes * 60) + seconds + (miliseconds / 1000));
        }

		public IActionResult Sell(int id)
        {
			PlayerData playerData = PlayerDal.Get(id);
            if (playerData.sellAvailableTime.CompareTo(DateTime.Now) < 0)
            {
			    ViewBag.headerSize = 1;
			    ViewBag.textColor = "success";
			    ViewBag.feedbackText = "Bin Sold!";
                ViewBag.test = "<img src=\"/Images/Coins.png\">";

                startTime = DateTime.Now;

				playerData.sellAvailableTime = DateTime.Now;
                playerData.sellAvailableTime = playerData.sellAvailableTime.AddSeconds(GetUpgradeValue("TruckDelay", playerData));
                playerData.Dollars += playerData.binValue * GetUpgradeValue("SellMultiplier", playerData);
                playerData.EmptyBin();
                PlayerDal.Update(playerData);
            }
            else
            {
				ViewBag.headerSize = 1;
				ViewBag.textColor = "danger";
				ViewBag.feedbackText = "Truck not back yet!";
			}
            return View("GamePage", GetNewPageData());
		}

        public IActionResult Leaderboard()
        {
            // sort algorythm goes here
            ViewBag.name = User.FindFirstValue(ClaimTypes.NameIdentifier);
			List<PlayerData> players = PlayerDal.GetAll();
            var sortedList = players.OrderByDescending(obj => obj.Dollars).ToList();
            return View(sortedList);
        }

		[Authorize]
		public IActionResult GamePage()
		{
            ViewBag.headerSize = 2;
			ViewBag.textColor = "primary";
			ViewBag.feedbackText = "Welcome to\nEXTREME RECYCLER";

			PlayerData pd = GetMatchingPlayerData();
            if (pd == null)
            {
                return PartialView("_LoginPartial");
                //return View()
            }
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
        public IActionResult ChangeName(string id)
        {
            PlayerData pd = GetMatchingPlayerData();
            pd.DisplayName = id;
            PlayerDal.Update(pd);
			return RedirectToAction("GamePage", "Game", GetNewPageData());
		}
	}
}
