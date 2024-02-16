using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models.Upgrades
{
	public class ValueUpgrade : Upgrade
	{
		public ValueUpgrade() { }
		public ValueUpgrade(string _name, string _image, float _cost, float _costScalar, float _value, float _valueScalar) : base(_id, _name, _image, _cost, _costScalar)
		{
			BaseValue = _value;
			ValueScalar = _valueScalar;
		}

		[Required] public float BaseValue { get; set; }
		[Required] public float ValueScalar { get; set; }

		public override float Execute()
		{
			return BaseValue * MathF.Pow(ValueScalar, CurrentLevel);
		}
	}
}
