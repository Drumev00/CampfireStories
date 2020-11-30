namespace CampfireStories.Server.Features.Story
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Features.Common;
	using Features.Story.Models;

	public interface IStoriesService
	{
		Task<ResultModel<string>> CreateStoryAsync(CreateStoryRequestModel model, string userId);

		Task<IEnumerable<ListingStoryResponseModel>> GetAll();

		Task<ResultModel<DetailsStoryResponseModel>> GetDetailsAsync(string storyId);

		Task<ResultModel<bool>> UpdateStoryAsync(UpdateStoryRequestModel model, string storyId, string userId);

		Task<ResultModel<bool>> DeleteStoryAsync(string storyId, string userId);

		Task<RateStoryResponseModel> Rate(string storyId, int rating);

		Task<IEnumerable<ListingStoryResponseModel>> GetAllByUserId(string userId);
	}
}
