namespace CampfireStories.Server.Features.StoryReports.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.StoryReport;
	public class UpdateStoryReportRequestModel
	{
		public string Title { get; set; }

		[MinLength(MinReportContent)]
		[MaxLength(MaxReportContent)]
		public string Content { get; set; }
	}
}
