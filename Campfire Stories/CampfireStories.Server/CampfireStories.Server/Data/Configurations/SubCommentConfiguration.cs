namespace CampfireStories.Server.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using CampfireStories.Server.Data.Models;

	public class SubCommentConfiguration : IEntityTypeConfiguration<SubComment>
	{
		public void Configure(EntityTypeBuilder<SubComment> entity)
		{
			entity
				.HasOne(sc => sc.User)
				.WithMany(u => u.SubComments)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			entity
				.HasOne(sc => sc.Comment)
				.WithMany(c => c.SubComments)
				.HasForeignKey(x => x.RootCommentId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
