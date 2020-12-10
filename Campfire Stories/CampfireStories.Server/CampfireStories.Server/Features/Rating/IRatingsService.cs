namespace CampfireStories.Server.Features.Rating
{
	using System.Threading.Tasks;
	using CampfireStories.Server.Features.Common;
	using Features.Rating.Models;

	public interface IRatingsService
	{
		Task<RateStoryResponseModel> Rate(string storyId, string userId, int rating);

		Task<bool> AlreadyRated(string storyId, string userId);
	}
}
