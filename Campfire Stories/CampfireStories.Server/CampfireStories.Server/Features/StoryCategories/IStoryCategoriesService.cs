namespace CampfireStories.Server.Features.StoryCategories
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IStoryCategoriesService
	{
		Task<string> CreateAsync(string storyId, IEnumerable<string> categoryIds);

		Task<string> UpdateAsync(string storyId, string[] categories);

		Task<string> DeleteAsync(string storyId);
	}
}
