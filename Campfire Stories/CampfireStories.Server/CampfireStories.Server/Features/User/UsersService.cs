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

	public class UsersService : IUsersService
	{
		private readonly UserManager<User> userManager;
		private readonly CampfireStoriesDbContext dbContext;

		public UsersService(UserManager<User> userManager, CampfireStoriesDbContext dbContext)
		{
			this.userManager = userManager;
			this.dbContext = dbContext;
		}

		public async Task<ResultModel<bool>> UpdateUser(string userId, UpdateUserRequestModel model)
		{
			var user = await GetById(userId);
			if (user == null)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}

			user.Biography = model.Biography;

			// In case the user deteles his display name. I need it because I render it on the front-end (the header).
			user.DisplayName = string.IsNullOrWhiteSpace(model.DisplayName) || model.DisplayName == "" ?
				user.DisplayName = user.UserName :
				user.DisplayName = model.DisplayName;

			user.Email = model.Email;
			user.ProfilePictureUrl = model.ProfilePictureUrl;

			this.dbContext.Update(user);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}

		public async Task<bool> IsAdminAsync(string userId)
		{
			var user = await this.userManager.FindByIdAsync(userId);
			
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

			this.dbContext.Users.Update(user);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true
			};
		}

		public async Task<bool> IsBanned(string userId)
		{
			var user = await this.userManager.FindByIdAsync(userId);

			return await this.userManager.IsInRoleAsync(user, BannedUserRoleName);
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

		public async Task<ResultModel<bool>> BanUser(string userId)
		{
			var user = await this.GetById(userId);
			if (user == null)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}
			await this.userManager.RemoveFromRoleAsync(user, RegularUserRoleName);
			await this.userManager.AddToRoleAsync(user, BannedUserRoleName);

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}

		private async Task<User> GetById(string id)
		{
			return await this.dbContext
				.Users
				.Where(u => u.Id == id && !u.IsDeleted)
				.FirstOrDefaultAsync();
		}

		public async Task<ResultModel<bool>> UnbanUser(string userId)
		{
			var user = await this.GetById(userId);
			if (user == null)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.InvalidUserId }
				};
			}
			await this.userManager.RemoveFromRoleAsync(user, BannedUserRoleName);
			await this.userManager.AddToRoleAsync(user, RegularUserRoleName);

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}

		public async Task<bool> ResetPhoto(string userId, string profilePictureUrl)
		{
			var user = await this.dbContext
				.Users
				.Where(u => u.Id == userId)
				.FirstOrDefaultAsync();

			if (string.IsNullOrWhiteSpace(profilePictureUrl))
			{
				profilePictureUrl = "https://res.cloudinary.com/dn2ouybbf/image/upload/v1606411713/by9buhmgr5ipeum4dzyr.jpg";
			}
			user.ProfilePictureUrl = profilePictureUrl;
			this.dbContext.Users.Update(user);
			await this.dbContext.SaveChangesAsync();

			return true;
		}
	}
}
