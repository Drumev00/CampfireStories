namespace CampfireStories.Server.Data.Models.Common
{
	using System;
	using System.Threading.Tasks;


	public interface ISeeder
	{
		public Task SeedAsync(CampfireStoriesDbContext db, IServiceProvider service);
	}
}
