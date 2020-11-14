namespace CampfireStories.Server.Features.User
{
	using System.Threading.Tasks;

	using Features.Common;
	using Features.User.Models;

	public interface IUsersService
	{
		Task<bool> IsAdminAsync(string userId);

		Task<ResultModel<bool>> BanUser(string userId);

		Task<ResultModel<bool>> UnbanUser(string userId);

		Task<ResultModel<bool>> UpdateUser(UpdateUserRequestModel model);

		Task<ResultModel<GetProfileResponseModel>> GetProfile(string userId);

		Task<ResultModel<bool>> DeleteUser(string userId);

		Task<bool> IsBanned(string userId);
	}
}
