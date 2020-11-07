namespace CampfireStories.Server.Features.Story
{
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Data.Models;
	using Features.User;
	using Features.Common;
	using Features.Story.Models;

	using static Features.Common.Errors;
	using System;
	using CampfireStories.Server.Features.StoryCategories;

	public class StoryService : IStoryService
	{
		private readonly CampfireStoriesDbContext dbContext;
		private readonly IUserService userService;
		private readonly IStoryCategoriesService storyCategoriesService;

		public StoryService(
			CampfireStoriesDbContext dbContext,
			IUserService userService,
			IStoryCategoriesService storyCategoriesService)
		{
			this.dbContext = dbContext;
			this.userService = userService;
			this.storyCategoriesService = storyCategoriesService;
		}

		public async Task<ResultModel<DetailsStoryResponseModel>> CreateStoryAsync(CreateStoryRequestModel model)
		{
			var author = await this.dbContext
				.Users
				.Where(u => u.Id == model.UserId && !u.IsDeleted)
				.FirstOrDefaultAsync();
			if (author == null || model.UserId == null)
			{
				return new ResultModel<DetailsStoryResponseModel>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}
			var isBanned = await this.userService.IsBanned(model.UserId);
			if (isBanned)
			{
				return new ResultModel<DetailsStoryResponseModel>
				{
					Errors = { UserErrors.BannedUserCreateStory }
				};
			}

			var story = new Story
			{
				Title = model.Title,
				Content = model.Content,
				PictureUrl = model.PictureUrl,
				UserId = model.UserId,
			};

			await this.dbContext.Stories.AddAsync(story);
			await this.dbContext.SaveChangesAsync();

			await this.storyCategoriesService.CreateAsync(story.Id, model.Categories);

			return await this.dbContext
				.Stories
				.Where(s => s.Id == story.Id)
				.Select(s => new ResultModel<DetailsStoryResponseModel>
				{
					Result = new DetailsStoryResponseModel
					{
						CreatedOn = s.CreatedOn,
						Title = s.Title,
						Content = s.Content,
						Username = s.User.UserName,
						PictureUrl = s.PictureUrl,
						Rating = s.Rating,
						Votes = s.Votes,
						Categories = model.Categories,
					},

					Success = true,
				})
				.FirstOrDefaultAsync();
		}

		public async Task<ResultModel<bool>> DeleteStoryAsync(string storyId, string userId, string loggedUser)
		{
			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == storyId && !s.IsDeleted)
				.FirstOrDefaultAsync();
			var isAdmin = await this.userService.IsAdminAsync(loggedUser);
			if (userId == null)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}
			if (story == null)
			{
				return new ResultModel<bool>
				{
					Errors = { StoryErrors.NotFoundOrDeletedStory }
				};
			}
			var isBanned = await this.userService.IsBanned(loggedUser);
			if (isBanned)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.BannedUserCreateStory }
				};
			}
			if (isAdmin || loggedUser == userId)
			{
				story.IsDeleted = true;
				story.DeletedOn = DateTime.UtcNow;

				await this.storyCategoriesService.DeleteAsync(storyId);

				this.dbContext.Update(story);
				await this.dbContext.SaveChangesAsync();

				return new ResultModel<bool>
				{
					Success = true,
				};
			}
			else
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}
		}

		public async Task<ResultModel<DetailsStoryResponseModel>> GetDetailsAsync(string storyId)
		{
			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == storyId && !s.IsDeleted)
				.Select(s => new ResultModel<DetailsStoryResponseModel>
				{
					Result = new DetailsStoryResponseModel
					{
						Title = s.Title,
						CreatedOn = s.CreatedOn,
						PictureUrl = s.PictureUrl,
						Content = s.Content,
						Username = s.User.UserName,
						Rating = s.Rating,
						Votes = s.Votes,
					},

					Success = true,
				})
				.FirstOrDefaultAsync();
			if (story == null)
			{
				return new ResultModel<DetailsStoryResponseModel>
				{
					Errors = { StoryErrors.NotFoundOrDeletedStory }
				};
			}

			return story;
		}

		public async Task<ResultModel<DetailsStoryResponseModel>> UpdateStoryAsync(UpdateStoryRequestModel model, string storyId)
		{
			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == storyId && !s.IsDeleted)
				.FirstOrDefaultAsync();
			if (story == null)
			{
				return new ResultModel<DetailsStoryResponseModel>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}
			var isBanned = await this.userService.IsBanned(story.UserId);
			if (isBanned)
			{
				return new ResultModel<DetailsStoryResponseModel>
				{
					Errors = { UserErrors.BannedUserCreateStory }
				};
			}


			// All of them aren't nullable.
			story.Title = model.Title == null ?
				model.Title = story.Title :
				story.Title = model.Title;

			story.Content = model.Content == null ?
				model.Content = story.Content :
				story.Content = model.Content;

			story.PictureUrl = model.PictureUrl == null ?
				model.PictureUrl = story.PictureUrl :
				story.PictureUrl = model.PictureUrl;

			story.ModifiedOn = DateTime.UtcNow;

			this.dbContext.Update(story);
			await this.storyCategoriesService.UpdateAsync(storyId, model.Categories);

			return new ResultModel<DetailsStoryResponseModel>
			{
				Result = new DetailsStoryResponseModel
				{
					Title = story.Title,
					CreatedOn = story.CreatedOn,
					PictureUrl = story.PictureUrl,
					Content = story.Content,
					Username = story.User.UserName,
					Rating = story.Rating,
					Votes = story.Votes,
					Categories = story.StoryCategories.Select(sc => sc.CategoryId).ToArray(),
				},
				Success = true,
			};
		}
	}
}
