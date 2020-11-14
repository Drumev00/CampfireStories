namespace CampfireStories.Server.Features.UserReports.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.UserReport;

	public class UpdateUserReportRequestModel
	{
		public string Title { get; set; }

		[MinLength(MinUserReportContent)]
		[MaxLength(MaxUserReportContent)]
		public string Content { get; set; }
	}
}
