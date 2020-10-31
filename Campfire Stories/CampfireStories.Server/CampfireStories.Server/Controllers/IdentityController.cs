using CampfireStories.Server.Data.Models;
using CampfireStories.Server.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CampfireStories.Server.Controllers
{
	public abstract class IdentityController : ApiController
	{
		private readonly UserManager<User> userManager;

		public IdentityController(UserManager<User> userManager)
		{
			this.userManager = userManager;
		}

		public async Task<IActionResult> Register(RegisterUserRequestModel model)
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
	}
}
