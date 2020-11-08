﻿namespace CampfireStories.Server.Features.Comment
{
	using System.Linq;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Data.Models;
	using Features.Comment.Models;
	using Features.Common;
	using Features.User;

	using static Features.Common.Errors;
	using System;

	public class CommentService : ICommentService
	{
		private readonly CampfireStoriesDbContext dbContext;
		private readonly IUserService userService;

		public CommentService(CampfireStoriesDbContext dbContext, IUserService userService)
		{
			this.dbContext = dbContext;
			this.userService = userService;
		}

		public async Task<ResultModel<CreateCommentResponseModel>> CreateCommentAsync(CreateCommentRequestModel model)
		{
			var user = await this.dbContext
				.Users
				.Where(u => u.Id == model.UserId && !u.IsDeleted)
				.FirstOrDefaultAsync();
			if (user == null)
			{
				return new ResultModel<CreateCommentResponseModel>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}

			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == model.StoryId && !s.IsDeleted)
				.FirstOrDefaultAsync();
			if (story == null)
			{
				return new ResultModel<CreateCommentResponseModel>
				{
					Errors = { StoryErrors.NotFoundOrDeletedStory }
				};
			}
			var isBanned = await this.userService.IsBanned(model.UserId);
			if (isBanned)
			{
				return new ResultModel<CreateCommentResponseModel>
				{
					Errors = { CommentErrors.BannedUserCreateComment }
				};
			}

			var comment = new Comment
			{
				Content = model.Content,
				StoryId = model.StoryId,
				UserId = model.UserId,
				Likes = 0,
				Dislikes = 0,
			};

			await this.dbContext.AddAsync(comment);
			await this.dbContext.SaveChangesAsync();

			return await this.dbContext
				.Comments
				.Where(c => c.Id == comment.Id)
				.Select(c => new ResultModel<CreateCommentResponseModel>
				{
					Result = new CreateCommentResponseModel
					{
						Content = c.Content,
						CreatedOn = c.CreatedOn,
						Likes = c.Likes,
						Dislikes = c.Dislikes,
						User = new UserCommentDetailsModel
						{
							UserId = c.User.Id,
							UserName = c.User.UserName,
							ProfilePic = c.User.ProfilePictureUrl,
						},
					},

					Success = true
				})
				.FirstOrDefaultAsync();
		}

		public async Task<ResultModel<bool>> DeleteCommentAsync(string commentId, string userId)
		{
			var isAdmin = await this.userService.IsAdminAsync(userId);
			if (!isAdmin)
			{
				return new ResultModel<bool>
				{
					Errors = { CommentErrors.UserHaveNoPermissionToDeleteCommetns }
				};
			}

			var comment = await this.dbContext
				.Comments
				.Where(c => c.Id == commentId && !c.IsDeleted)
				.FirstOrDefaultAsync();

			comment.IsDeleted = true;
			comment.DeletedOn = DateTime.UtcNow;

			this.dbContext.Update(comment);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Success = true,
			};
		}

		public async Task<IEnumerable<CreateCommentResponseModel>> GetAllByStoryId(string storyId)
		{
			return await this.dbContext
				.Comments
				.Where(c => c.StoryId == storyId && !c.IsDeleted)
				.Select(c => new CreateCommentResponseModel
				{
					Content = c.Content,
					CreatedOn = c.CreatedOn,
					Likes = c.Likes,
					Dislikes = c.Dislikes,
					User = new UserCommentDetailsModel
					{
						UserId = c.User.Id,
						UserName = c.User.UserName,
						ProfilePic = c.User.ProfilePictureUrl,
					},
				})
				.OrderByDescending(c => c.Likes)
				.ToListAsync();
		}

		public async Task<ResultModel<bool>> UpdateCommentAsync(UpdateCommentRequestModel model, string commentId, string loggedUser, bool isAdmin)
		{
			var isBanned = await this.userService.IsBanned(model.UserId);
			if (isBanned)
			{
				return new ResultModel<bool>
				{
					Errors = { CommentErrors.BannedUserCreateComment }
				};
			}
			var comment = await this.dbContext
				.Comments
				.Where(c => c.Id == commentId &&
					!c.IsDeleted)
				.FirstOrDefaultAsync();
			if (comment == null)
			{
				return new ResultModel<bool>
				{
					Errors = { CommentErrors.NotFoundOrDeletedComment }
				};
			}

			if (loggedUser != model.UserId && !isAdmin)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}
			comment.Content = model.Content;

			this.dbContext.Update(comment);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Success = true,
			};
		}
	}
}
