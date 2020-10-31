namespace CampfireStories.Server.Data.Configurations
{
	using CampfireStories.Server.Data.Models;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> entity)
		{
			entity
				.HasOne(c => c.User)
				.WithMany(u => u.Comments)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			entity
				.HasOne(c => c.Story)
				.WithMany(s => s.Comments)
				.HasForeignKey(x => x.StoryId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
