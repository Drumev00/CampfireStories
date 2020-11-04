namespace CampfireStories.Server.Features
{
	using Microsoft.AspNetCore.Mvc;

	public class HomeController : ApiController
	{
		//[Authorize]
		public IActionResult Get()
		{
			return Ok("Works");
		}
	}
}
