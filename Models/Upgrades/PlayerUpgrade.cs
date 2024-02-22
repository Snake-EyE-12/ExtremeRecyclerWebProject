using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models.Upgrades
{
    public class PlayerUpgrade
    {
        public PlayerUpgrade() { }
        public PlayerUpgrade(string user, int id, int lvl)
        {
            AssociatedUser = user;
            UpgradeID = id;
            UpgradeLevel = lvl;
        }
        [Key] public int ID { get; set; }
        [Required] public string? AssociatedUser { get; set; }
        [Required] public int UpgradeID { get; set; }
        [Required] public int UpgradeLevel { get; set; }
    }
}
