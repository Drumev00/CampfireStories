namespace CampfireStories.Server.Features.UserReports.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.UserReport;
	public class CreateUserReportRequestModel
	{
		[Required]
		public string Title { get; set; }

		[Required]
		[MinLength(MinUserReportContent)]
		[MaxLength(MaxUserReportContent)]
		public string Content { get; set; }

		[Required]
		public string ReportedId { get; set; }
	}
}
