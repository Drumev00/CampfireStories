namespace CampfireStories.Server.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using CampfireStories.Server.Data.Models;


	public class StoryCategoriesConfiguration : IEntityTypeConfiguration<StoryCategories>
	{
		public void Configure(EntityTypeBuilder<StoryCategories> entity)
		{
			entity.HasKey(sc => new { sc.CategoryId, sc.StoryId });
		}
	}
}
