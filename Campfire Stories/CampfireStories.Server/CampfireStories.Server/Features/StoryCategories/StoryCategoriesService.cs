namespace CampfireStories.Server.Features.StoryCategories
{
	using System.Linq;
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using Microsoft.EntityFrameworkCore;

	using Data;

	using static Features.Common.Errors;
	using CampfireStories.Server.Data.Models;
	using System;

	public class StoryCategoriesService : IStoryCategoriesService
	{
		private readonly CampfireStoriesDbContext dbContext;

		public StoryCategoriesService(CampfireStoriesDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<string> CreateAsync(string storyId, IEnumerable<string> categoryIds)
		{
			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == storyId && !s.IsDeleted)
				.FirstOrDefaultAsync();
			if (story == null)
			{
				return StoryErrors.NotFoundOrDeletedStory;
			}

			foreach (var id in categoryIds)
			{
				var storyCategory = new StoryCategories
				{
					StoryId = story.Id,
					CategoryId = id
				};

				await this.dbContext.StoryCategories.AddAsync(storyCategory);
			}

			await this.dbContext.SaveChangesAsync();

			return story.Id;
		}

		public async Task<string> DeleteAsync(string storyId)
		{
			var storyCategories = await this.dbContext.
				StoryCategories
				.Where(sc => sc.StoryId == storyId && !sc.IsDeleted)
				.ToListAsync();
			if (storyCategories == null)
			{
				return StoryErrors.NotFoundOrDeletedStory;
			}

			foreach (var storyCategory in storyCategories)
			{
				storyCategory.IsDeleted = true;
				storyCategory.DeletedOn = DateTime.UtcNow;
			}
			this.dbContext.UpdateRange(storyCategories);
			await this.dbContext.SaveChangesAsync();

			return storyId;
		}

		public async Task<string> UpdateAsync(string storyId, string[] categories)
		{
			var storyCategories = await this.dbContext
				.StoryCategories
				.Where(s => s.StoryId == storyId && !s.IsDeleted)
				.ToListAsync();

			foreach (var storyCategory in storyCategories)
			{
				this.dbContext.Remove(storyCategory);
			}

			await this.CreateAsync(storyId, categories);
			await this.dbContext.SaveChangesAsync();

			return storyCategories.Count.ToString();
		}
	}
}
