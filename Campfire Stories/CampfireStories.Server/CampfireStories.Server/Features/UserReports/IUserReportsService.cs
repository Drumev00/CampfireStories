namespace CampfireStories.Server.Features.UserReports
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Features.Common;
	using Features.UserReports.Models;

	public interface IUserReportsService
	{
		Task<ResultModel<bool>> ReportUserAsync(CreateUserReportRequestModel model, string reporterId);

		Task<IEnumerable<ListingUserReportsResponseModel>> GetAllByUserId(bool read, string userId);

		Task<IEnumerable<ListingUserReportsResponseModel>> GetAllForAdmin(bool read);

		Task<ResultModel<GetUserReportDetailsResponseModel>> GetDetailsById(string userReportId);

		Task<ResultModel<bool>> UpdateReportAsync(UpdateUserReportRequestModel model, string userReportId, string userId);

		Task<ResultModel<bool>> DeleteReportAsync(string userReportId, string userId);
	}
}
