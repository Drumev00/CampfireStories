namespace CampfireStories.Server.Features.Category
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Data.Models;
	using Features.User;
	using Features.Common;
	using Features.Category.Models;

	using static Features.Common.Errors;

	public class CategoryService : ICategoryService
	{
		private readonly CampfireStoriesDbContext dbContext;
		private readonly IUserService userService;

		public CategoryService(CampfireStoriesDbContext dbContext, IUserService userService)
		{
			this.dbContext = dbContext;
			this.userService = userService;
		}

		public async Task<ResultModel<string>> CreateCategoryAsync(string name, string userId)
		{
			var isAdmin = await this.userService.IsAdminAsync(userId);
			if (!isAdmin)
			{
				return new ResultModel<string>
				{
					Errors = { CategoryErrors.NoPermissionToCreateCategory }
				};
			}

			if (string.IsNullOrWhiteSpace(name) || name == null)
			{
				return new ResultModel<string>
				{
					Errors = { CategoryErrors.CategoryNameMustHaveValue }
				};
			}


			var category = new Category
			{
				Name = name,
			};

			await this.dbContext.AddAsync(category);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<string> 
			{ 
				Result = category.Id ,
				Success = true,
			};

		}

		public async Task<ResultModel<bool>> DeleteCategoryAsync(string categoryId, string userId)
		{
			var isAdmin = await this.userService.IsAdminAsync(userId);
			if (!isAdmin)
			{
				return new ResultModel<bool>
				{
					Errors = { CategoryErrors.NoPermissionToCreateCategory }

				};
			}

			var category = await this.dbContext
				.Categories
				.Where(c => c.Id == categoryId)
				.FirstOrDefaultAsync();

			category.DeletedOn = DateTime.UtcNow;
			category.IsDeleted = true;
			this.dbContext.Update(category);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Success = true,
			};
		}

		public async Task<IEnumerable<CategoryListingModel>> GetAll()
		{
			return await this.dbContext
				.Categories
				.Where(c => !c.IsDeleted)
				.Select(c => new CategoryListingModel
				{
					Name = c.Name,
				})
				.ToListAsync();
		}

		public async Task<ResultModel<bool>> UpdateCategoryAsync(string newName, string categoryId, string userId)
		{
			var isAdmin = await this.userService.IsAdminAsync(userId);
			if (!isAdmin)
			{
				return new ResultModel<bool>
				{
					Errors = { CategoryErrors.NoPermissionToCreateCategory }

				};
			}

			var category = this.dbContext
				.Categories
				.Where(c => c.Id == categoryId)
				.FirstOrDefault();

			if (category == null)
			{
				return new ResultModel<bool>
				{
					Errors = { CategoryErrors.CategoryNameMustHaveValue }

				};
			}

			category.Name = newName;
			category.ModifiedOn = DateTime.UtcNow;
			this.dbContext.Update(category);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Success = true,
			};
		}
	}
}
