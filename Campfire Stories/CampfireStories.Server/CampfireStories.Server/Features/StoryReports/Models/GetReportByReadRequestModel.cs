namespace CampfireStories.Server.Features.StoryReports.Models
{
	using System.ComponentModel.DataAnnotations;

	public class GetReportByReadRequestModel
	{
		[Required]
		public bool Read { get; set; }
	}
}
