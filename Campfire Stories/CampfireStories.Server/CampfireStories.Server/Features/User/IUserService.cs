namespace CampfireStories.Server.Features.User
{
	using System.Threading.Tasks;

	using Features.Common;
	using Features.User.Models;

	public interface IUserService
	{
		Task<bool> IsAdminAsync(string userId);

		// Will make this after the UserReport / StoryReport CRUD is done so I can use the report as a reason for the ban.
		// Task<bool> BanUser(string userId);

		Task<ResultModel<UpdateUserResponseModel>> UpdateUser(UpdateUserRequestModel model);

		Task<ResultModel<bool>> DeleteUser(string userId);
	}
}
