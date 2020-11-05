namespace CampfireStories.Server.Features.Identity
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Options;

	using Data.Models;
	using Features.Identity.Models;

	public class IdentityController : ApiController
	{
		private readonly UserManager<User> userManager;
		private readonly IIdentityService identityService;
		private readonly AppSettings appSettings;

		public IdentityController(
			UserManager<User> userManager,
			IOptions<AppSettings> appSettings,
			IIdentityService identityService)
		{
			this.userManager = userManager;
			this.identityService = identityService;
			this.appSettings = appSettings.Value;
		}

		[HttpPost]
		[Route(nameof(Register))]
		public async Task<ActionResult> Register(RegisterUserRequestModel model)
		{
			var user = new User
			{
				Email = model.Email,
				UserName = model.UserName
			};

			var result = await this.userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				return Ok();
			}

			return BadRequest(result.Errors);
		}

		[HttpPost]
		[Route(nameof(Login))]
		public async Task<ActionResult<object>> Login(LoginUserRequestModel model)
		{
			var user = await userManager.FindByNameAsync(model.Username);
			if (user == null)
			{
				return Unauthorized();
			}

			var validatePassword = await this.userManager.CheckPasswordAsync(user, model.Password);
			if (!validatePassword)
			{
				return Unauthorized();
			}

			var encryptedToken = this.identityService.GenerateJwtToken(user.Id, user.UserName, appSettings.Secret);

			return new LoginResponseModel
			{
				Token = encryptedToken
			};
		}
	}
}
