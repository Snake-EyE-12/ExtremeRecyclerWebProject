using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExtremeRecycler.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Item> Items { get; set; }
		public DbSet<ValueUpgrade> Upgrades { get; set; }
		public DbSet<PlayerData> PlayerData { get; set; }
		public DbSet<PlayerUpgrade> PlayerUpgrades { get; set; }
	}
}