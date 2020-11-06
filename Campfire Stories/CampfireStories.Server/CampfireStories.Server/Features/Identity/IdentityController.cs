namespace CampfireStories.Server.Features.Identity
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Options;

	using Features.Identity.Models;
	using Features.Common;

	using static Features.ApiRoutes;
	using Microsoft.AspNetCore.Authorization;
	using CampfireStories.Server.Infrastructure;

	public class IdentityController : ApiController
	{
		private readonly IIdentityService identityService;
		private readonly AppSettings appSettings;

		public IdentityController(
			IOptions<AppSettings> appSettings,
			IIdentityService identityService)
		{
			this.identityService = identityService;
			this.appSettings = appSettings.Value;
		}

		[HttpPost]
		[AllowAnonymous]
		[Route(IdentityRoutes.Register)]
		public async Task<ActionResult<ResultModel<RegisterResponseModel>>> Register(RegisterUserRequestModel model)
		{
			var result = await this.identityService.RegisterAsync(
				model.UserName,
				model.Password,
				model.Gender,
				model.Email,
				this.appSettings.Secret);

			if (!result.Success)
			{
				return BadRequest(new { result.Errors });
			}

			return result;
		}

		[HttpPost]
		[AllowAnonymous]
		[Route(IdentityRoutes.Login)]
		public async Task<ActionResult<ResultModel<LoginResponseModel>>> Login(LoginUserRequestModel model)
		{
			var result = await this.identityService.LoginAsync(model.Username, model.Password, appSettings.Secret);
			if (!result.Success)
			{
				return this.Unauthorized(new { result.Errors });
			}

			return result;
		}
	}
}
