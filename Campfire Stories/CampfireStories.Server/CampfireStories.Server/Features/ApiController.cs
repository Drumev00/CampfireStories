namespace CampfireStories.Server.Features
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using static Data.Models.Common.Constants.Roles;

	[ApiController]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public abstract class ApiController : ControllerBase
	{

	}
}
