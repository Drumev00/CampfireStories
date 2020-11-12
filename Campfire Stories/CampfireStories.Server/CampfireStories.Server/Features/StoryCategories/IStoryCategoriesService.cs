namespace CampfireStories.Server.Features.StoryCategories
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Features.Common;

	public interface IStoryCategoriesService
	{
		Task<string> CreateAsync(string storyId, IEnumerable<string> categoryIds);

		Task<string> UpdateAsync(string storyId, string[] categories);

		Task<ResultModel<string[]>> GetAllByStoryId(string storyId);

		Task<string> DeleteAsync(string storyId);
	}
}
