using ExtremeRecycler.Models.Upgrades;

namespace ExtremeRecycler.Models
{
    public class BigModel
    {
        public BigModel() { }
        public BigModel(PlayerData playerdata, Item item, IEnumerable<ValueUpgrade> upgrades)
        {
            this.playerData = playerdata;
            this.Item = item;
            this.PlayerUpgrades = upgrades;
        }
        public PlayerData playerData { get; set; }
        public Item Item { get; set; }
        public IEnumerable<ValueUpgrade> PlayerUpgrades { get; set; }
    }
}
