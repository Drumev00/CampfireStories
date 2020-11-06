namespace CampfireStories.Server.Data.Models.Common
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class DbContextSeeder : ISeeder
	{
		public async Task SeedAsync(CampfireStoriesDbContext db, IServiceProvider service)
		{
			if(db == null)
			{
				throw new ArgumentNullException(nameof(db));
			}

			if (service == null)
			{
				throw new ArgumentNullException(nameof(service));
			}

			var seeders = new List<ISeeder>
			{
				new RolesSeeder(),
				new DataSeeder(),
			};

			foreach (var seeder in seeders)
			{
				await seeder.SeedAsync(db, service);
				await db.SaveChangesAsync();
			}
		}
	}
}
