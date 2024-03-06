using ExtremeRecycler.Models.Upgrades;

namespace ExtremeRecycler.Models
{
    public class BigModel
    {
        public BigModel() { }
        public BigModel(PlayerData playerdata, Item item, IEnumerable<ValueUpgrade> upgrades, IEnumerable<PlayerData> allplayerdata)
        {
            this.playerData = playerdata;
            this.Item = item;
            this.PlayerUpgrades = upgrades;
            this.AllPlayerData = allplayerdata;
        }

        public BigModel(PlayerData playerdata, Item item)
        {
            this.playerData = playerdata;
            this.Item = item;
        }

        public PlayerData playerData { get; set; }
        public Item Item { get; set; }
        public IEnumerable<PlayerData> AllPlayerData{ get; set; }
        public IEnumerable<ValueUpgrade> PlayerUpgrades { get; set; }
    }
}
