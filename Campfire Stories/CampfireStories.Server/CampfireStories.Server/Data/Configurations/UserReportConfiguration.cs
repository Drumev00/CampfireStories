namespace CampfireStories.Server.Data.Configurations
{
	using Microsoft.EntityFrameworkCore;

	using Data.Models;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class UserReportConfiguration : IEntityTypeConfiguration<UserReport>
	{
		public void Configure(EntityTypeBuilder<UserReport> entity)
		{
			entity
				.HasOne(ur => ur.Reporter)
				.WithMany(u => u.UserReports)
				.HasForeignKey(x => x.ReporterId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
