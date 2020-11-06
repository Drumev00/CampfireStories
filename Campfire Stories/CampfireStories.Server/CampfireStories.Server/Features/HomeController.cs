namespace CampfireStories.Server.Features
{
	using Microsoft.AspNetCore.Mvc;

	public class HomeController : ApiController
	{
		//[Authorize]
		[HttpGet]
		[Route("[controller]")]
		public IActionResult Get()
		{
			return Ok("Works");
		}
	}
}
