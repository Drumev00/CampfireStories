namespace CampfireStories.Server.Features.Identity
{
	using System.Threading.Tasks;

	using Features.Common;
	using Features.Identity.Models;

	public interface IIdentityService
	{
		string GenerateJwtToken(string userId, string userName, string secret);

		Task<ResultModel<LoginResponseModel>> LoginAsync(string username, string password, string secret);

		Task<ResultModel<RegisterResponseModel>> RegisterAsync(string username, string password, string gender, string email, string secret);
	}
}
