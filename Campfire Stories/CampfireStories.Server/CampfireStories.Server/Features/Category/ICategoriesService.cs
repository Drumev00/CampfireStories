namespace CampfireStories.Server.Features.Category
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Features.Category.Models;
	using Features.Common;

	public interface ICategoriesService
	{
		Task<ResultModel<string>> CreateCategoryAsync(string name, string userId);

		Task<IEnumerable<CategoryListingModel>> GetAll();

		Task<ResultModel<bool>> UpdateCategoryAsync(string newName, string categoryId, string userId);

		Task<ResultModel<bool>> DeleteCategoryAsync(string categoryId, string userId);
	}
}
