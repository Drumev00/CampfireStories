namespace CampfireStories.Server.Models.Identity
{
	using System.ComponentModel.DataAnnotations;


	public class LoginUserRequestModel
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
