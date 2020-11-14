namespace CampfireStories.Server.Features.UserReports
{
	using System.Linq;
	using System.Threading.Tasks;
	using System.Collections.Generic;

	using Data;
	using Data.Models;
	using Features.User;
	using Features.Common;
	using Features.UserReports.Models;

	using static Features.Common.Errors;
	using Microsoft.EntityFrameworkCore;
	using System;

	public class UserReportsService : IUserReportsService
	{
		private readonly CampfireStoriesDbContext dbContext;
		private readonly IUsersService usersService;

		public UserReportsService(CampfireStoriesDbContext dbContext, IUsersService usersService)
		{
			this.dbContext = dbContext;
			this.usersService = usersService;
		}

		public async Task<ResultModel<bool>> DeleteReportAsync(string userReportId, string userId)
		{
			var userReport = await this.dbContext
				.UserReports
				.Where(ur => ur.Id == userReportId && !ur.IsDeleted)
				.FirstOrDefaultAsync();
			if (userReport == null)
			{
				return new ResultModel<bool>
				{
					Errors = { StoryReportErrors.ReportNotFoundOrDeleted }
				};
			}
			if (userId != userReport.ReporterId)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}

			userReport.IsDeleted = true;
			userReport.DeletedOn = DateTime.UtcNow;

			this.dbContext.UserReports.Update(userReport);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}

		public async Task<IEnumerable<ListingUserReportsResponseModel>> GetAllByUserId(bool read, string userId)
		{
			return await this.dbContext
				.UserReports
				.Where(
					ur => ur.IsRead == read &&
					!ur.IsDeleted && 
					ur.ReporterId == userId)
				.Select(ur => new ListingUserReportsResponseModel
				{
					Id = ur.Id,
					Title = ur.Title,
					Content = ur.Content.Substring(0, 90) + "...",
					Reporter = new UserDetailsReportModel
					{
						UserId = ur.Reporter.Id,
						UserName = ur.Reporter.UserName,
					},
					Reported = new UserDetailsReportModel
					{
						UserId = ur.Reported.Id,
						UserName = ur.Reported.UserName,
					},
				})
				.ToListAsync();
		}

		public async Task<IEnumerable<ListingUserReportsResponseModel>> GetAllForAdmin(bool read)
		{
			return await this.dbContext
				.UserReports
				.Where(ur => ur.IsRead == read && !ur.IsDeleted)
				.Select(ur => new ListingUserReportsResponseModel
				{
					Id = ur.Id,
					Title = ur.Title,
					Content = ur.Content.Substring(0, 90) + "...",
					Reporter = new UserDetailsReportModel
					{
						UserId = ur.Reporter.Id,
						UserName = ur.Reporter.UserName,
					},
					Reported = new UserDetailsReportModel
					{
						UserId = ur.Reported.Id,
						UserName = ur.Reported.UserName,
					},
				})
				.ToListAsync();
		}

		public async Task<ResultModel<GetUserReportDetailsResponseModel>> GetDetailsById(string userReportId)
		{
			var userReport = await this.dbContext
				.UserReports
				.Where(ur => ur.Id == userReportId && !ur.IsDeleted)
				.Select(ur => new ResultModel<GetUserReportDetailsResponseModel>
				{
					Result = new GetUserReportDetailsResponseModel
					{
						Id = ur.Id,
						CreatedOn = ur.CreatedOn,
						Title = ur.Title,
						Content = ur.Content,
						Reporter = new UserDetailsReportModel
						{
							UserId = ur.Reporter.Id,
							UserName = ur.Reporter.UserName,
							ProfilePic = ur.Reporter.ProfilePictureUrl,
						},
						Reported = new UserDetailsReportModel
						{
							UserId = ur.Reporter.Id,
							UserName = ur.Reporter.UserName,
							ProfilePic = ur.Reporter.ProfilePictureUrl,
						},
					},

					Success = true,
				})
				.FirstOrDefaultAsync();
			if (userReport == null)
			{
				return new ResultModel<GetUserReportDetailsResponseModel>
				{
					Errors = { StoryReportErrors.ReportNotFoundOrDeleted }
				};
			}

			return userReport;
		}

		public async Task<ResultModel<bool>> ReportUserAsync(CreateUserReportRequestModel model, string reporterId)
		{
			var isBanned = await this.usersService.IsBanned(reporterId);
			if (isBanned)
			{
				return new ResultModel<bool>
				{
					Errors = { StoryReportErrors.BannedUserReports }
				};
			}

			var userReport = new UserReport
			{
				Title = model.Title,
				Content = model.Content,
				IsRead = false,
				ReportedId = model.ReportedId,
				ReporterId = reporterId,
			};

			await this.dbContext.UserReports.AddAsync(userReport);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}

		public async Task<ResultModel<bool>> UpdateReportAsync(UpdateUserReportRequestModel model, string userReportId, string userId)
		{
			var userReport = await this.dbContext
				.UserReports
				.Where(ur => ur.Id == userReportId && !ur.IsDeleted)
				.FirstOrDefaultAsync();
			if (userReport == null)
			{
				return new ResultModel<bool>
				{
					Errors = { StoryReportErrors.ReportNotFoundOrDeleted }
				};
			}
			if (userId != userReport.ReporterId)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}

			userReport.Title = model.Title == null || string.IsNullOrWhiteSpace(model.Title) ?
				model.Title = userReport.Title :
				userReport.Title = model.Title;

			userReport.Content = model.Content == null || string.IsNullOrWhiteSpace(model.Content) ?
				model.Content = userReport.Content :
				userReport.Content = model.Content;

			userReport.ModifiedOn = DateTime.UtcNow;

			this.dbContext.UserReports.Update(userReport);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}
	}
}
