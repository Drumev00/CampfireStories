namespace CampfireStories.Server.Data.Models.Common
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Linq;
	using System.Threading.Tasks;

    using static Data.Models.Common.Constants.Roles;

	public class RolesSeeder : ISeeder
	{
		public async Task SeedAsync(CampfireStoriesDbContext db, IServiceProvider service)
		{
            var roleManager = service.GetRequiredService<RoleManager<Role>>();

            await SeedRoleAsync(roleManager, AdministratorRoleName);
            await SeedRoleAsync(roleManager, RegularUserRoleName);
            await SeedRoleAsync(roleManager, BannedUserRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<Role> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new Role(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
