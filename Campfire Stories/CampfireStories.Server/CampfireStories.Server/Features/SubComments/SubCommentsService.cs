namespace CampfireStories.Server.Features.SubComments
{
	using System.Linq;
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using Data;
	using Data.Models;
	using Features.User;
	using Features.Common;
	using Features.Comment.Models;
	using Features.SubComments.Models;

	using static Features.Common.Errors;
	using Microsoft.EntityFrameworkCore;
	using System;

	public class SubCommentsService : ISubCommentsService
	{
		private readonly CampfireStoriesDbContext dbContext;
		private readonly IUsersService usersService;

		public SubCommentsService(CampfireStoriesDbContext dbContext, IUsersService usersService)
		{
			this.dbContext = dbContext;
			this.usersService = usersService;
		}

		public async Task<ResultModel<string>> CreateAsync(SubCommentCreateRequestModel model, string userId)
		{
			if (model.RootCommentId == null || string.IsNullOrWhiteSpace(model.RootCommentId))
			{
				return new ResultModel<string>
				{
					Errors = { SubCommentErrors.NotFoundOrDeletedSubComment }
				};
			}
			if (userId == null || string.IsNullOrWhiteSpace(userId))
			{
				return new ResultModel<string>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}
			var isBanned = await this.usersService.IsBanned(userId);
			if (isBanned)
			{
				return new ResultModel<string>
				{
					Errors = { CommentErrors.BannedUserCreateComment }
				};
			}

			var subComment = new SubComment
			{
				Content = model.Content,
				UserId = userId,
				RootCommentId = model.RootCommentId
			};

			await this.dbContext.AddAsync(subComment);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<string>
			{
				Result = subComment.Id,
				Success = true,
			};
		}

		public async Task<ResultModel<bool>> DeleteAsync(string subCommentid, string userId)
		{
			var subComment = await this.dbContext
				.SubComments
				.Where(sc => sc.Id == subCommentid && !sc.IsDeleted)
				.FirstOrDefaultAsync();
			if (subComment == null)
			{
				return new ResultModel<bool>
				{
					Errors = { SubCommentErrors.NotFoundOrDeletedSubComment }
				};
			}
			var isAdmin = await this.usersService.IsAdminAsync(userId);

			if (!isAdmin)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate}
				};
			}


			subComment.IsDeleted = true;
			subComment.DeletedOn = DateTime.UtcNow;

			this.dbContext.Update(subComment);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}

		public async Task<IEnumerable<SubCommentListingResponseModel>> GetAllByRootCommentId(string rootCommentId)
		{
			return await this.dbContext
				.SubComments
				.Where(c => c.RootCommentId == rootCommentId && !c.IsDeleted)
				.Select(c => new SubCommentListingResponseModel
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
				.ToListAsync();
		}

		public async Task<ResultModel<bool>> UpdateAsync(UpdateSubCommentRequestModel model, string subCommentId, string userId)
		{
			var subComment = await this.dbContext
				.SubComments
				.Where(sc => sc.Id == subCommentId && !sc.IsDeleted)
				.FirstOrDefaultAsync();
			if (subComment == null)
			{
				return new ResultModel<bool>
				{
					Errors = { SubCommentErrors.NotFoundOrDeletedSubComment }
				};
			}
			if (userId != subComment.UserId)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}

			subComment.Content = model.Content;
			subComment.ModifiedOn = DateTime.UtcNow;

			this.dbContext.Update(subComment);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}
	}
}
