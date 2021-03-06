﻿namespace CampfireStories.Server.Features.Identity
{
	using System;
	using System.Linq;
	using System.Text;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using System.IdentityModel.Tokens.Jwt;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.AspNetCore.Identity;

	using Data.Models;
	using Features.Common;
	using Features.Identity.Models;
	using Data.Enumerations;

	using static Features.Common.Errors;
	using static Data.Models.Common.Constants.Roles;
	using CampfireStories.Server.Data;
	using Microsoft.EntityFrameworkCore;

	public class IdentityService : IIdentityService
	{
		private readonly UserManager<User> userManager;
		private readonly CampfireStoriesDbContext dbContext;

		public IdentityService(UserManager<User> userManager, CampfireStoriesDbContext dbContext)
		{
			this.userManager = userManager;
			this.dbContext = dbContext;
		}

		public string GenerateJwtToken(string userId, string userName, string secret)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, userId),
					new Claim(ClaimTypes.Name, userName)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var encryptedToken = tokenHandler.WriteToken(token);

			return encryptedToken;
		}

		public async Task<ResultModel<LoginResponseModel>> LoginAsync(string username, string password, string secret)
		{
			// Getting user using UserManager via username includes the deleted ones.
			var user = await this.dbContext
				.Users
				.Where(u => u.UserName == username)
				.FirstOrDefaultAsync();
			if (user == null)
			{
				return new ResultModel<LoginResponseModel>
				{
					Errors = { string.Format(Identity.UserNotFound, username) }
				};
			}

			var validPass = await userManager.CheckPasswordAsync(user, password);
			if (!validPass)
			{
				return new ResultModel<LoginResponseModel>
				{
					Errors = { Identity.PasswordsDontMatch }
				};
			}
			var displayName = string.IsNullOrWhiteSpace(user.DisplayName) ? user.DisplayName = user.UserName : user.DisplayName;
			var encryptedToken = this.GenerateJwtToken(user.Id, user.UserName, secret);
			var isAdmin = await this.userManager.IsInRoleAsync(user, AdministratorRoleName);
			return new ResultModel<LoginResponseModel>
			{
				Result = new LoginResponseModel
				{
					UserId = user.Id,
					UserName = username,
					Token = encryptedToken,
					IsAdmin = isAdmin,
					DisplayName = displayName,
					ProfilePictureUrl = user.ProfilePictureUrl,
				},

				Success = true,
			};

		}

		public async Task<ResultModel<RegisterResponseModel>> RegisterAsync(
			string username,
			string password,
			string gender,
			string email,
			string secret)
		{
			var existingUser = await userManager.FindByNameAsync(username);
			if (existingUser != null)
			{
				return new ResultModel<RegisterResponseModel>
				{
					Errors = { string.Format(Identity.UserAlreadyExists, username) }
				};
			}

			var user = new User
			{
				ProfilePictureUrl = "https://res.cloudinary.com/dn2ouybbf/image/upload/v1606411713/by9buhmgr5ipeum4dzyr.jpg",
				UserName = username,
				Email = email,
				DisplayName = username,
				Gender = (Gender)Enum.Parse(typeof(Gender), gender),
			};

			var register = await this.userManager.CreateAsync(user, password);
			if (!register.Succeeded)
			{
				return new ResultModel<RegisterResponseModel>
				{
					Errors = register.Errors.Select(x => x.Description).ToList(),
				};
			}

			var token = GenerateJwtToken(user.Id, user.UserName, secret);
			await this.userManager.AddToRoleAsync(user, RegularUserRoleName);
			return new ResultModel<RegisterResponseModel>
			{
				Result = new RegisterResponseModel
				{
					UserName = user.UserName,
					Email = user.Email,
					Gender = user.Gender.ToString(),
					Token = token
				},

				Success = true,
			};
		}
	}
}
