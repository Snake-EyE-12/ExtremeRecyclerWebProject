using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models.Upgrades
{
    public abstract class Upgrade
    {
        [Key] public int ID { get; set; }
        [Required] public string? UpgradeName { get; set; }
        [Required] public string? BackgroundImage { get; set; }
		[Required] public float BaseCost { get; set; }
		[Required] public float CostScalar { get; set; }
		[Required] public int CurrentLevel { get; set; }


        public bool AttemptPurchase(ref float networth)
        {
            if(CanPurchase(networth))
            {
                networth -= GetCurrentCost();
                CurrentLevel++;
                return true;
            }
            return false;
        }
        private bool CanPurchase(float networth)
        {
            return (networth >= GetCurrentCost());
        }
        private float GetCurrentCost()
        {
            return (BaseCost * MathF.Pow(CostScalar, CurrentLevel));
        }
        protected virtual void Execute()
        {
            
        }
    }
}
