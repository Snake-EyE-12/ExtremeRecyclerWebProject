using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models.Upgrades
{
	public class ValueUpgrade
	{
		public ValueUpgrade() { }
		public ValueUpgrade(string _name, string _image, float _cost, float _costScalar, float _value, float _valueScalar)
		{
			UpgradeName = _name;
			BackgroundImage = _image;
			BaseCost = _cost;
			CostScalar = _costScalar;

			BaseValue = _value;
			ValueScalar = _valueScalar;
		}

		[Key] public int ID { get; set; }
		[Required] public string? DisplayName { get; set; }
		[Required] public string? UpgradeName { get; set; }
		[Required] public string? Description { get; set; }
		[Required] public string? BackgroundImage { get; set; }
		[Required] public float BaseCost { get; set; }
		[Required] public float CostScalar { get; set; }
		[Required] public int CurrentLevel { get; set; } = 0;

		[Required] public float BaseValue { get; set; }
		[Required] public float ValueScalar { get; set; }

		public bool AttemptPurchase(ref float networth)
		{
			if (CanPurchase(networth))
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
		public float GetCurrentCost()
		{
			return (BaseCost * MathF.Pow(CostScalar, CurrentLevel));
		}
		public float Execute()
		{
			return BaseValue * MathF.Pow(ValueScalar, CurrentLevel);
		}
	}
}
