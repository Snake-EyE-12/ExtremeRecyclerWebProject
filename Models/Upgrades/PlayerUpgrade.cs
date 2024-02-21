using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models.Upgrades
{
    public class PlayerUpgrade
    {
        [Key] public int ID { get; set; }
        [Required] public string? AssociatedUser { get; set; }
        [Required] public int UpgradeID { get; set; }
        [Required] public int UpgradeLevel { get; set; }
    }
}
