using System.ComponentModel.DataAnnotations;

namespace ExtremeRecycler.Models
{
	public class Account
	{
		[Key] public int Id { get; set; }
		[Required] public string Username { get; set; } = "";
		[Required] public string Password { get; set; } = "";
	}
}
