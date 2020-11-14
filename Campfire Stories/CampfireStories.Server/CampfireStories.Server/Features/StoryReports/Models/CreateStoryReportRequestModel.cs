namespace CampfireStories.Server.Features.StoryReports.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.StoryReport;
	public class CreateStoryReportRequestModel
	{
		[Required]
		public string Title { get; set; }

		[Required]
		[MinLength(MinReportContent)]
		[MaxLength(MaxReportContent)]
		public string Content { get; set; }

		[Required]
		public string StoryId { get; set; }
	}
}
