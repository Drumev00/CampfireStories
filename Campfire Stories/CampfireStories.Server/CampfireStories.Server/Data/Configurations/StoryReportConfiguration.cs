namespace CampfireStories.Server.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using CampfireStories.Server.Data.Models;

	public class StoryReportConfiguration : IEntityTypeConfiguration<StoryReport>
	{
		public void Configure(EntityTypeBuilder<StoryReport> entity)
		{
			entity
				.HasOne(sr => sr.Reporter)
				.WithMany(u => u.StoryReports)
				.HasForeignKey(x => x.ReporterId)
				.OnDelete(DeleteBehavior.Restrict);

			entity
				.HasOne(sr => sr.Story)
				.WithMany(s => s.StoryReports)
				.HasForeignKey(x => x.StoryId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
