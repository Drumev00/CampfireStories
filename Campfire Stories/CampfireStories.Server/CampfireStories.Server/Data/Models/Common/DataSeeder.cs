namespace CampfireStories.Server.Data.Models.Common
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;

	using Data.Enumerations;

	using static Data.Models.Common.Constants.AdminCredentials;
	using static Data.Models.Common.Constants.Roles;
	using Microsoft.EntityFrameworkCore;

	public class DataSeeder : ISeeder
	{
		public async Task SeedAsync(CampfireStoriesDbContext db, IServiceProvider service)
		{
			var userManager = service.GetRequiredService<UserManager<User>>();

			var users = await userManager.Users.AnyAsync();
			if (!users)
			{
				await CreateAdmin(userManager, AdminUsername, AdminEmail, AdminGender);
			}
		}

		private static async Task<string> CreateAdmin(
			UserManager<User> userManager,
			string username,
			string email,
			string gender)
		{
			var user = new User
			{
				UserName = username,
				Email = email,
				Gender = (Gender)Enum.Parse(typeof(Gender), gender)
			};

			var pass = AdminPassword;
			await userManager.CreateAsync(user, pass);
			await userManager.AddToRoleAsync(user, AdministratorRoleName);

			return user.Id;
		}
	}
}
