namespace CampfireStories.Server.Features.User
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Data.Models;
	using Features.Common;
	using Features.User.Models;

	using static Data.Models.Common.Constants.Roles;
	using static Features.Common.Errors;

	public class UserService : IUserService
	{
		private readonly UserManager<User> userManager;
		private readonly CampfireStoriesDbContext dbContext;

		public UserService(UserManager<User> userManager, CampfireStoriesDbContext dbContext)
		{
			this.userManager = userManager;
			this.dbContext = dbContext;
		}

		public async Task<ResultModel<UpdateUserResponseModel>> UpdateUser(UpdateUserRequestModel model)
		{
			var user = await GetById(model.UserId);
			if (user == null)
			{
				return new ResultModel<UpdateUserResponseModel>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}

			user.Biography = model.Biography;
			user.DisplayName = model.DisplayName;
			user.ProfilePictureUrl = model.ProfilePictureUrl;

			this.dbContext.Update(user);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<UpdateUserResponseModel>
			{
				Result = new UpdateUserResponseModel
				{
					UserName = user.UserName,
					Email = user.Email,
					CreatedOn = user.CreatedOn,
					Gender = user.Gender.ToString(),
					Biography = model.Biography,
					DisplayName = model.DisplayName,
					ProfilePictureUrl = model.ProfilePictureUrl,
				},

				Success = true,
			};
		}

		public async Task<bool> IsAdminAsync(string userId)
		{
			if (userId == null || string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException(Identity.InvalidUser);
			}

			var user = await this.userManager.FindByIdAsync(userId);
			if (user == null || user.IsDeleted)
			{
				throw new ArgumentNullException(Identity.InvalidUser);
			}

			return await this.userManager.IsInRoleAsync(user, AdministratorRoleName);
		}

		public async Task<ResultModel<bool>> DeleteUser(string userId)
		{
			var user = await GetById(userId);
			if (user == null)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}

			user.IsDeleted = true;
			user.DeletedOn = DateTime.UtcNow;

			this.dbContext.Update(user);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Success = true
			};
		}

		public async Task<bool> IsBanned(string userId)
		{
			var user = await this.userManager.FindByIdAsync(userId);

			return await this.userManager.IsInRoleAsync(user, BannedUserRoleName);
		}

		private async Task<User> GetById(string id)
		{
			return await this.dbContext
				.Users
				.Where(u => u.Id == id && !u.IsDeleted)
				.FirstOrDefaultAsync();
		}

		public async Task<ResultModel<GetProfileResponseModel>> GetProfile(string userId)
		{
			var user = await this.dbContext
				.Users
				.Where(u => u.Id == userId && !u.IsDeleted)
				.FirstOrDefaultAsync();
			if (user == null)
			{
				return new ResultModel<GetProfileResponseModel>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}

			return new ResultModel<GetProfileResponseModel>
			{
				Result = new GetProfileResponseModel
				{
					UserName = user.UserName,
					Email = user.Email,
					DisplayName = user.DisplayName,
					CreatedOn = user.CreatedOn,
					Gender = user.Gender.ToString(),
					Biography = user.Biography,
					ProfilePictureUrl = user.ProfilePictureUrl,
				},

				Success = true,
			};
		}
	}
}
