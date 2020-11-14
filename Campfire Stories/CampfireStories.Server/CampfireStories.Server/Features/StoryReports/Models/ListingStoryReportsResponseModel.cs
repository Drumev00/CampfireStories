namespace CampfireStories.Server.Features.StoryReports.Models
{
	using System;

	public class ListingStoryReportsResponseModel
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public string StoryTitle { get; set; }

		public UserDetailsReportModel User { get; set; }
	}
}
