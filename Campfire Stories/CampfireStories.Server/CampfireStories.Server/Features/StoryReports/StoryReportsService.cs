namespace CampfireStories.Server.Features.StoryReports
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
	using Features.StoryReports.Models;

	using static Features.Common.Errors;

	public class StoryReportsService : IStoryReportsService
	{
		private readonly CampfireStoriesDbContext dbContext;
		private readonly IUsersService usersService;

		public StoryReportsService(CampfireStoriesDbContext dbContext, IUsersService usersService)
		{
			this.dbContext = dbContext;
			this.usersService = usersService;
		}

		public async Task<IEnumerable<ListingStoryReportsResponseModel>> GetAll(bool read)
		{
			return await this.dbContext
				.StoryReports
				.Where(sr => sr.IsRead == read)
				.Select(sr => new ListingStoryReportsResponseModel
				{
					Id = sr.Id,
					Title = sr.Title,
					Content = sr.Content.Substring(0, 90) + "...",
					StoryTitle = sr.Story.Title,
					User = new UserDetailsReportModel
					{
						UserId = sr.ReporterId,
						ProfilePic = sr.Reporter.ProfilePictureUrl,
						UserName = sr.Reporter.UserName,
					},
				})
				.ToListAsync();
		}

		public async Task<ResultModel<bool>> ReportStoryAsync(CreateStoryReportRequestModel model, string reporterId)
		{
			var isBanned = await this.usersService.IsBanned(reporterId);
			if (isBanned)
			{
				return new ResultModel<bool>
				{
					Errors = { StoryReportErrors.BannedUserReports }
				};
			}

			var storyReport = new StoryReport
			{
				Title = model.Title,
				Content = model.Content,
				IsRead = false,
				ReporterId = reporterId,
				StoryId = model.StoryId,
			};

			await this.dbContext.StoryReports.AddAsync(storyReport);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}

		public async Task<ResultModel<DetailsStoryReportResponseModel>> GetDetailsById(string reportId)
		{
			var report = await this.dbContext
				.StoryReports
				.Where(sr => sr.Id == reportId && !sr.IsDeleted)
				.Select(sr => new ResultModel<DetailsStoryReportResponseModel>
				{
					Result = new DetailsStoryReportResponseModel
					{
						Id = sr.Id,
						CreatedOn = sr.CreatedOn,
						Title = sr.Title,
						Content = sr.Content,
						User = new UserDetailsReportModel
						{
							UserId = sr.ReporterId,
							UserName = sr.Reporter.UserName,
							ProfilePic = sr.Reporter.ProfilePictureUrl,
						},
					},

					Success = true,
				})
				.FirstOrDefaultAsync();
			if (report == null)
			{
				return new ResultModel<DetailsStoryReportResponseModel>
				{
					Errors = { StoryReportErrors.ReportNotFoundOrDeleted }
				};
			}

			return report;
		}

		public async Task<ResultModel<bool>> UpdateStoryReportAsync(UpdateStoryReportRequestModel model, string userId, string storyReportId)
		{
			var report = await this.dbContext
				.StoryReports
				.Where(sr => sr.Id == storyReportId && !sr.IsDeleted)
				.FirstOrDefaultAsync();
			if (report == null)
			{
				return new ResultModel<bool>
				{
					Errors = { StoryReportErrors.ReportNotFoundOrDeleted }
				};
			}
			if (userId != report.ReporterId)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}

			report.Title = model.Title == null ?
				model.Title = report.Title :
				report.Title = model.Title;

			report.Content = model.Content == null ?
				model.Content = report.Content :
				report.Content = model.Content;

			report.ModifiedOn = DateTime.UtcNow;

			this.dbContext.StoryReports.Update(report);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}

		public async Task<ResultModel<bool>> DeleteStoryReportAsync(string userId, string storyReportId)
		{
			var report = await this.dbContext
				.StoryReports
				.Where(sr => sr.Id == storyReportId && !sr.IsDeleted)
				.FirstOrDefaultAsync();
			if (report == null)
			{
				return new ResultModel<bool>
				{
					Errors = { StoryReportErrors.ReportNotFoundOrDeleted }
				};
			}
			if (userId != report.ReporterId)
			{
				return new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				};
			}

			report.IsDeleted = true;
			report.DeletedOn = DateTime.UtcNow;

			this.dbContext.StoryReports.Update(report);
			await this.dbContext.SaveChangesAsync();

			return new ResultModel<bool>
			{
				Result = true,
				Success = true,
			};
		}
	}
}
