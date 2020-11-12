namespace CampfireStories.Server.Features.User
{
	using System.Threading.Tasks;

	using Features.Common;
	using Features.User.Models;

	public interface IUsersService
	{
		Task<bool> IsAdminAsync(string userId);

		// Will make this after the UserReport / StoryReport CRUD is done so I can use the report as a reason for the ban.
		// Task<bool> BanUser(string userId);

		Task<ResultModel<UpdateUserResponseModel>> UpdateUser(UpdateUserRequestModel model);

		Task<ResultModel<GetProfileResponseModel>> GetProfile(string userId);

		Task<ResultModel<bool>> DeleteUser(string userId);

		Task<bool> IsBanned(string userId);
	}
}
