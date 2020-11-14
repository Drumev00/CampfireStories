namespace CampfireStories.Server.Features.UserReports.Models
{
	public class ListingUserReportsResponseModel
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public UserDetailsReportModel Reporter { get; set; }

		public UserDetailsReportModel Reported { get; set; }
	}
}
