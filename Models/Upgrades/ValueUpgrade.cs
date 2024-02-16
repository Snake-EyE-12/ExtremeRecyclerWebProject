using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models.Upgrades
{
	public class ValueUpgrade : Upgrade
	{
		public ValueUpgrade() { }
		public ValueUpgrade(int _id, string _name, string _image, float _cost, float _costScalar, float _time, float _timeScalar)// : base(_id, _name, _image, _cost, _scalar)
		{
			BaseValue = _time;
			ValueScalar = _timeScalar;
		}

		[Required] public float BaseValue { get; set; }
		[Required] public float ValueScalar { get; set; }

		public override float Execute()
		{
			return BaseValue * MathF.Pow(ValueScalar, CurrentLevel);
		}
	}
}
