namespace CampfireStories.Server.Features.Story
{
	using System.Threading.Tasks;

	using Features.Common;
	using Features.Story.Models;

	public interface IStoriesService
	{
		Task<ResultModel<string>> CreateStoryAsync(CreateStoryRequestModel model, string userId);

		Task<ResultModel<DetailsStoryResponseModel>> GetDetailsAsync(string storyId);

		Task<ResultModel<bool>> UpdateStoryAsync(UpdateStoryRequestModel model, string storyId, string userId);

		Task<ResultModel<bool>> DeleteStoryAsync(string storyId, string userId);
	}
}
