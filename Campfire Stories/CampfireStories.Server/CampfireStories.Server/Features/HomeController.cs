namespace CampfireStories.Server.Features
{
	using Microsoft.AspNetCore.Mvc;

	public class HomeController : ApiController
	{
		//[Authorize]
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Works");
		}
	}
}
