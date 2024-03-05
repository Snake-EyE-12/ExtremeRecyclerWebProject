using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models.Upgrades
{
	public class ValueUpgrade
	{
		public ValueUpgrade() { }
		public ValueUpgrade(int id, string _name, string displayName, string desc, string _image, float _cost, float _costScalar, float _value, float _valueScalar, int lvl)
		{
			UpgradeName = _name;
			DisplayName = displayName;
			Description = desc;
			BackgroundImage = _image;
			BaseCost = _cost;
			CostScalar = _costScalar;

			matchingID = id;

			BaseValue = _value;
			ValueScalar = _valueScalar;

			CurrentLevel = lvl;
		}

		[Key] public int ID { get; set; }
		public int matchingID;
		[Required] public string? DisplayName { get; set; }
		[Required] public string? UpgradeName { get; set; }
		[Required] public string? Description { get; set; }
		[Required] public string? BackgroundImage { get; set; }
		[Required] public float BaseCost { get; set; }
		[Required] public float CostScalar { get; set; }
		[Required] public int CurrentLevel { get; set; }

		[Required] public float BaseValue { get; set; }
		[Required] public float ValueScalar { get; set; }

		public bool AttemptPurchase(PlayerData player)
		{
			if (CanPurchase(player.Dollars))
			{
				player.Dollars -= GetCurrentCost();
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
			return (float)Math.Round(BaseCost * MathF.Pow(CostScalar, CurrentLevel), 2);
		}
		public float Execute()
		{
			return (float)Math.Round(BaseValue * MathF.Pow(ValueScalar, CurrentLevel), 2);
		}
	}
}
