namespace CampfireStories.Server.Features.StoryReports.Models
{
	using System;

	public class DetailsStoryReportResponseModel
	{
		public string Id { get; set; }

		public DateTime CreatedOn { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public UserDetailsReportModel User { get; set; }
	}
}
