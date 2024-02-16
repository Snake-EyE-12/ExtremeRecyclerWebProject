using ExtremeRecycler.Models.Upgrades;

namespace ExtremeRecycler.Models
{
    public class BigModel
    {
        public BigModel() { }
        public BigModel(PlayerData playerdata, Item item)
        {
            this.playerData = playerdata;
            this.Item = item;
        }
        public PlayerData playerData { get; set; }
        public Item Item { get; set; }
        public List<Upgrade> upgradeShop {  get; set; }
    }
}
