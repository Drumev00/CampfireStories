namespace CampfireStories.Server.Features.StoryReports
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Features.User;
	using Infrastructure;
	using Features.StoryReports.Models;

	using static ApiRoutes;
	using static Features.Common.Errors;

	public class StoryReportsController : ApiController
	{
		private readonly IStoryReportsService storyReportsService;
		private readonly IUsersService usersService;

		public StoryReportsController(IStoryReportsService storyReportsService, IUsersService usersService)
		{
			this.storyReportsService = storyReportsService;
			this.usersService = usersService;
		}

		[HttpPost]
		[Route(StoryReportRoutes.Create)]
		public async Task<ActionResult> CreateReport(CreateStoryReportRequestModel model)
		{
			var loggedUser = this.User.GetId();
			var result = await this.storyReportsService.ReportStoryAsync(model, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpGet]
		[Route(StoryReportRoutes.AllReadOrNot)]
		public async Task<ActionResult> AllByRead(bool read)
		{
			var loggedUser = this.User.GetId();
			var isAdmin = await this.usersService.IsAdminAsync(loggedUser);
			if (!isAdmin)
			{
				return Unauthorized(StoryReportErrors.AdminPermission);
			}
			var result = await this.storyReportsService.GetAll(read);

			return Ok(result);
		}

		[HttpGet]
		[Route(StoryReportRoutes.GetDetailsById)]
		public async Task<ActionResult> GetDetailsById(string storyReportId)
		{
			var loggedUser = this.User.GetId();
			var isAdmin = await this.usersService.IsAdminAsync(loggedUser);
			if (!isAdmin)
			{
				return Unauthorized(StoryReportErrors.AdminPermission);
			}

			var result = await this.storyReportsService.GetDetailsById(storyReportId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpPut]
		[Route(StoryReportRoutes.Update)]
		public async Task<ActionResult> UpdateStoryReport(UpdateStoryReportRequestModel model, string storyReportId)
		{
			var loggedUser = this.User.GetId();
			var result = await this.storyReportsService.UpdateStoryReportAsync(model, loggedUser, storyReportId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpDelete]
		[Route(StoryReportRoutes.Delete)]
		public async Task<ActionResult> DeleteStoryReport(string storyReportId)
		{
			var loggedUser = this.User.GetId();
			var result = await this.storyReportsService.DeleteStoryReportAsync(loggedUser, storyReportId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

	}
}
