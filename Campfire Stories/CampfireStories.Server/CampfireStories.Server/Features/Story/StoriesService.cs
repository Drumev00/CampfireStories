namespace CampfireStories.Server.Features.Story
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Data.Models;
	using Features.User;
	using Features.Common;
	using Features.Comment;
	using Features.Story.Models;
	using Features.StoryCategories;

	using static Features.Common.Errors;
	using static Data.Models.Common.Constants.Story;
	using System.Collections.Generic;

	public class StoriesService : IStoriesService
	{
		private readonly CampfireStoriesDbContext dbContext;
		private readonly IUsersService userService;
		private readonly IStoryCategoriesService storyCategoriesService;
		private readonly ICommentsService commentService;

		public StoriesService(
			CampfireStoriesDbContext dbContext,
			IUsersService userService,
			IStoryCategoriesService storyCategoriesService,
			ICommentsService commentService)
		{
			this.dbContext = dbContext;
			this.userService = userService;
			this.storyCategoriesService = storyCategoriesService;
			this.commentService = commentService;
		}

		public async Task<ResultModel<string>> CreateStoryAsync(CreateStoryRequestModel model, string userId)
		{
			var author = await this.dbContext
				.Users
				.Where(u => u.Id == userId && !u.IsDeleted)
				.FirstOrDefaultAsync();
			if (author == null)
			{
				return new ResultModel<string>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}
			var isBanned = await this.userService.IsBanned(userId);
			if (isBanned)
			{
				return new ResultModel<string>
				{
					Errors = { UserErrors.BannedUserCreateStory }
				};
			}

			var story = new Story
			{
				Title = model.Title,
				Content = model.Content,
				PictureUrl = model.PictureUrl,
				Rating = 0,
				UserId = userId,
			};

			await this.dbContext.Stories.AddAsync(story);
			await this.dbContext.SaveChangesAsync();

			await this.storyCategoriesService.CreateAsync(story.Id, model.Categories);

			return new ResultModel<string>
			{
				Result = story.Id,
				Success = true,
			};
		}

		public async Task<ResultModel<bool>> DeleteStoryAsync(string storyId, string userId)
		{
			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == storyId && !s.IsDeleted)
				.FirstOrDefaultAsync();
			var isAdmin = await this.userService.IsAdminAsync(userId);
			if (story == null)
			{
				return new ResultModel<bool>
				{
					Errors = { StoryErrors.NotFoundOrDeletedStory }
				};
			}
			var isBanned = await this.userService.IsBanned(userId);
			if (isBanned)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.BannedUserCreateStory }
				};
			}
			if (!isAdmin && userId != story.UserId)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}
			story.IsDeleted = true;
			story.DeletedOn = DateTime.UtcNow;

			await this.storyCategoriesService.DeleteAsync(storyId);

			this.dbContext.Stories.Update(story);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Success = true,
			};
		}


		public async Task<ListingPaginationStories> GetAll(int? take, int skip, string title)
		{
			var total = this.dbContext
				.Stories
				.Count();

			var query = this.dbContext
				.Stories
				.OrderByDescending(s => s.CreatedOn)
				.Skip(skip)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(title))
			{
				query = query.Where(s => s.Title.ToLower().Contains(title));
			}

			if (take.HasValue)
			{
				query = query.Take(take.Value);
			}

			var stories = await query
				.Select(s => new IndividualStory
				{
					Id = s.Id,
					Title = s.Title,
					Content = s.Content.Substring(0, 350) + "...",
					PictureUrl = s.PictureUrl,
					UserId = s.UserId,
					UserName = s.User.UserName,
				})
				.ToListAsync();

			var result = new ListingPaginationStories
			{
				TotalItems = total,
				Stories = stories,
				TotalPages = (int)Math.Ceiling((double)total / StoriesPerPage),
			};

			return result;
		}

		public async Task<IEnumerable<ListingStoryResponseModel>> GetAllByForeignUsername(string username)
		{
			var result = await this.dbContext
				.Stories
				.Where(s => s.User.UserName == username)
				.OrderByDescending(s => s.CreatedOn)
				.Select(s => new ListingStoryResponseModel
				{
					Id = s.Id,
					Title = s.Title,
					Content = s.Content.Substring(0, 350) + "...",
					PictureUrl = s.PictureUrl,
				})
				.ToListAsync();

			return result;
		}

		public async Task<IEnumerable<ListingStoryResponseModel>> GetAllByUserId(string userId)
		{
			var userStories = await this.dbContext
				.Stories
				.Where(s => s.UserId == userId)
				.OrderByDescending(s => s.CreatedOn)
				.Select(s => new ListingStoryResponseModel
				{
					Id = s.Id,
					Title = s.Title,
					Content = s.Content.Substring(0, 350) + "...",
					PictureUrl = s.PictureUrl,
					UserName = s.User.UserName,
				})
				.ToListAsync();

			return userStories;
		}

		public async Task<ResultModel<DetailsStoryResponseModel>> GetDetailsAsync(string storyId)
		{
			if (storyId == null || string.IsNullOrWhiteSpace(storyId))
			{
				return new ResultModel<DetailsStoryResponseModel>
				{
					Errors = { StoryErrors.NotFoundOrDeletedStory }
				};
			}
			var categoryIds = await this.storyCategoriesService.GetAllByStoryId(storyId);
			var comments = await this.commentService.GetAllByStoryId(storyId);

			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == storyId && !s.IsDeleted)
				.Select(s => new ResultModel<DetailsStoryResponseModel>
				{
					Result = new DetailsStoryResponseModel
					{
						Id = s.Id,
						Title = s.Title,
						CreatedOn = s.CreatedOn,
						PictureUrl = s.PictureUrl,
						Content = s.Content,
						UserId = s.UserId,
						Username = s.User.UserName,
						Rating = s.Rating,
						Votes = s.Votes,
						Categories = categoryIds.Result,
						Comments = comments.ToList(),
					},

					Success = true,
				})
				.FirstOrDefaultAsync();

			return story;
		}

		public async Task<ResultModel<bool>> UpdateStoryAsync(UpdateStoryRequestModel model, string storyId, string userId)
		{
			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == storyId && !s.IsDeleted)
				.FirstOrDefaultAsync();
			if (story == null)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}
			var isBanned = await this.userService.IsBanned(story.UserId);
			if (isBanned)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.BannedUserCreateStory }
				};
			}
			if (userId != story.UserId)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}


			// All of them aren't nullable.
			story.Title = model.Title;

			story.Content = model.Content;

			story.PictureUrl = model.PictureUrl;

			story.ModifiedOn = DateTime.UtcNow;

			this.dbContext.Update(story);
			await this.storyCategoriesService.UpdateAsync(storyId, model.Categories);

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}
	}
}
