
namespace CampfireStories.Server.Data.Configurations
{
	using CampfireStories.Server.Data.Models;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class StoryConfiguration : IEntityTypeConfiguration<Story>
	{
		public void Configure(EntityTypeBuilder<Story> entity)
		{
			entity
				.HasOne(s => s.User)
				.WithMany(c => c.Stories)
				.HasForeignKey(u => u.UserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
