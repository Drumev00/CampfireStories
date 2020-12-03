namespace CampfireStories.Server.Features.Comment
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
	using Features.SubComments;
	using Features.Comment.Models;

	using static Features.Common.Errors;

	public class CommentsService : ICommentsService
	{
		private readonly CampfireStoriesDbContext dbContext;
		private readonly IUsersService userService;
		private readonly ISubCommentsService subCommentsService;

		public CommentsService(
			CampfireStoriesDbContext dbContext,
			IUsersService userService,
			ISubCommentsService subCommentsService)
		{
			this.dbContext = dbContext;
			this.userService = userService;
			this.subCommentsService = subCommentsService;
		}

		public async Task<ResultModel<string>> CreateCommentAsync(CreateCommentRequestModel model, string userId)
		{
			var user = await this.dbContext
				.Users
				.Where(u => u.Id == userId && !u.IsDeleted)
				.FirstOrDefaultAsync();
			if (user == null)
			{
				return new ResultModel<string>
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
				return new ResultModel<string>
				{
					Errors = { StoryErrors.NotFoundOrDeletedStory }
				};
			}
			var isBanned = await this.userService.IsBanned(userId);
			if (isBanned)
			{
				return new ResultModel<string>
				{
					Errors = { CommentErrors.BannedUserCreateComment }
				};
			}

			var comment = new Comment
			{
				Content = model.Content,
				StoryId = model.StoryId,
				UserId = userId,
				Likes = 0,
				Dislikes = 0,
			};

			await this.dbContext.AddAsync(comment);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<string>
			{
				Result = comment.Id,
				Success = true,
			};
		}

		public async Task<ResultModel<bool>> DeleteCommentAsync(string commentId, string userId)
		{
			var isAdmin = await this.userService.IsAdminAsync(userId);
			if (!isAdmin)
			{
				return new ResultModel<bool>
				{
					Errors = { CommentErrors.UserHaveNoPermissionToDeleteComments }
				};
			}

			var comment = await this.dbContext
				.Comments
				.Where(c => c.Id == commentId && !c.IsDeleted)
				.FirstOrDefaultAsync();

			await this.subCommentsService.DeleteAllByRootCommentIdAsync(commentId);

			comment.IsDeleted = true;
			comment.DeletedOn = DateTime.UtcNow;

			this.dbContext.Update(comment);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Success = true,
			};
		}

		public async Task<int> Dislike(string commentId)
		{
			var comment = await this.dbContext
				.Comments
				.Where(c => c.Id == commentId)
				.FirstOrDefaultAsync();

			comment.Dislikes++;

			this.dbContext.Comments.Update(comment);
			await this.dbContext.SaveChangesAsync();

			return comment.Dislikes;
		}

		public async Task<IEnumerable<CreateCommentResponseModel>> GetAllByStoryId(string storyId)
		{
			var comments = await this.dbContext
				.Comments
				.Where(c => c.StoryId == storyId && !c.IsDeleted)
				.Select(c => new CreateCommentResponseModel
				{
					Id = c.Id,
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
				.ThenByDescending(c => c.CreatedOn)
				.ToListAsync();
			foreach (var comment in comments)
			{
				var subComments = await this.subCommentsService.GetAllByRootCommentId(comment.Id);
				comment.SubComments = subComments;
			}

			return comments;
		}

		public async Task<CreateCommentResponseModel> GetById(string commentId)
		{
			var comment = await this.dbContext
				.Comments
				.Where(c => c.Id == commentId)
				.Select(c => new CreateCommentResponseModel
				{
					Id = c.Id,
					Content = c.Content,
					CreatedOn = c.CreatedOn,
					Likes = c.Likes,
					Dislikes = c.Dislikes,
				})
				.FirstOrDefaultAsync();

			return comment;
		}

		public async Task<int> Like(string commentId)
		{
			var comment = await this.dbContext
				.Comments
				.Where(c => c.Id == commentId)
				.FirstOrDefaultAsync();

			comment.Likes++;

			this.dbContext.Comments.Update(comment);
			await this.dbContext.SaveChangesAsync();

			return comment.Likes;
		}

		public async Task<ResultModel<string>> UpdateCommentAsync(string content, string commentId, string loggedUser)
		{
			var isBanned = await this.userService.IsBanned(loggedUser);
			if (isBanned)
			{
				return new ResultModel<string>
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
				return new ResultModel<string>
				{
					Errors = { CommentErrors.NotFoundOrDeletedComment }
				};
			}

			if (loggedUser != comment.UserId)
			{
				return new ResultModel<string>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}
			comment.Content = content;

			this.dbContext.Comments.Update(comment);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<string>
			{
				Result = content,
				Success = true,
			};
		}
	}
}
