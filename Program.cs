using ExtremeRecycler.Controllers;
using ExtremeRecycler.Data;
using ExtremeRecycler.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExtremeRecycler.Models;
using ExtremeRecycler.Models.Upgrades;
using ExtremeRecycler.Data.DALs;

namespace ExtremeRecycler
{
	public class Program
	{
		public static void Main(string[] args)
		{
			TimerController timerController = new TimerController();

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddControllersWithViews();

			builder.Services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 8;
				options.Password.RequiredUniqueChars = 0;

				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = false;

				//options.User.AllowedUserNameCharacters = "abc";
				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedAccount = false;
				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;
			});

			builder.Services.AddTransient<DataAccessLayer<Item>, ItemDataList>();
			builder.Services.AddTransient<DataAccessLayer<ValueUpgrade>, UpgradeDataList>();
			builder.Services.AddTransient<DataAccessLayer<PlayerData>, PlayerDataList>();
			builder.Services.AddTransient<DataAccessLayer<PlayerUpgrade>, PlayerUpgradeDataList>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Game}/{action=GamePage}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}