namespace CampfireStories.Server.Features.UserReports
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	using Features.User;
	using Infrastructure;
	using Features.UserReports.Models;

	using static ApiRoutes;
	using static Features.Common.Errors;

	public class UserReportsController : ApiController
	{
		private readonly IUserReportsService userReportsService;
		private readonly IUsersService usersService;

		public UserReportsController(IUserReportsService userReportsService, IUsersService usersService)
		{
			this.userReportsService = userReportsService;
			this.usersService = usersService;
		}

		[HttpPost]
		[Route(UserReportRoutes.Create)]
		public async Task<ActionResult> Create(CreateUserReportRequestModel model)
		{
			var loggedUser = this.User.GetId();
			var result = await this.userReportsService.ReportUserAsync(model, loggedUser);
			if (!result.Success)
			{
				return Unauthorized(result.Errors);
			}

			return Created(nameof(Create), result.Result);
		}

		[HttpGet]
		[Route(UserReportRoutes.AllByRead)]
		public async Task<ActionResult> GetAllByRead(bool read)
		{
			var loggedUser = this.User.GetId();
			var result = await this.userReportsService.GetAllByUserId(read, loggedUser);

			return Ok(result);
		}

		[HttpGet]
		[Route(UserReportRoutes.Details)]
		public async Task<ActionResult> GetDetails(string userReportId)
		{
			var loggedUser = this.User.GetId();
			var isAdmin = await this.usersService.IsAdminAsync(loggedUser);
			if (!isAdmin)
			{
				return Unauthorized(StoryReportErrors.AdminPermission);
			}

			var result = await this.userReportsService.GetDetailsById(userReportId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpPut]
		[Route(UserReportRoutes.Update)]
		public async Task<ActionResult> UpdateReport(UpdateUserReportRequestModel model, string userReportId)
		{
			var loggedUser = this.User.GetId();
			var result = await this.userReportsService.UpdateReportAsync(model, userReportId, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpDelete]
		[Route(UserReportRoutes.Delete)]
		public async Task<ActionResult> DeleteReport(string userReportId)
		{
			var loggedUser = this.User.GetId();
			var result = await this.userReportsService.DeleteReportAsync(userReportId, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpGet]
		[Route(UserReportRoutes.AdminListing)]
		public async Task<ActionResult> AdminGetAll(bool read)
		{
			var loggedUser = this.User.GetId();
			var isAdmin = await this.usersService.IsAdminAsync(loggedUser);
			if (!isAdmin)
			{
				return Unauthorized(StoryReportErrors.AdminPermission);
			}

			var result = await this.userReportsService.GetAllForAdmin(read);

			return Ok(result);
		}

	}
}
