namespace CampfireStories.Server.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using Data.Models;

	public class RatingsConfiguration : IEntityTypeConfiguration<Rating>
	{
		public void Configure(EntityTypeBuilder<Rating> entity)
		{
			entity.HasKey(r => new { r.StoryId, r.UserId });
		}
	}
}
