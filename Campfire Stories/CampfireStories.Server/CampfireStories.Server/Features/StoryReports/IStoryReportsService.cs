namespace CampfireStories.Server.Features.StoryReports
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Features.Common;
	using Features.StoryReports.Models;

	public interface IStoryReportsService
	{
		Task<ResultModel<bool>> ReportStoryAsync(CreateStoryReportRequestModel model, string reporterId);

		Task<IEnumerable<ListingStoryReportsResponseModel>> GetAll(bool read);

		Task<ResultModel<DetailsStoryReportResponseModel>> GetDetailsById(string reportId);

		Task<ResultModel<bool>> UpdateStoryReportAsync(UpdateStoryReportRequestModel model, string userId, string storyReportId);

		Task<ResultModel<bool>> DeleteStoryReportAsync(string userId, string storyReportId);
	}
}
