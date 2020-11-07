namespace CampfireStories.Server.Features.Story
{
	using System.Threading.Tasks;

	using Features.Common;
	using Features.Story.Models;

	public interface IStoryService
	{
		Task<ResultModel<DetailsStoryResponseModel>> CreateStoryAsync(CreateStoryRequestModel model);

		Task<ResultModel<DetailsStoryResponseModel>> GetDetailsAsync(string storyId);

		Task<ResultModel<DetailsStoryResponseModel>> UpdateStoryAsync(UpdateStoryRequestModel model, string storyId);

		Task<ResultModel<bool>> DeleteStoryAsync(string storyId, string userId, string loggedUser);
	}
}
