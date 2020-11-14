namespace CampfireStories.Server.Features.UserReports.Models
{
	using System;

	public class GetUserReportDetailsResponseModel
	{
		public string Id { get; set; }

		public DateTime CreatedOn { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public UserDetailsReportModel Reporter { get; set; }

		public UserDetailsReportModel Reported { get; set; }
	}
}
