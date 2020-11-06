namespace CampfireStories.Server.Features.Identity.Models
{
	public class LoginResponseModel
	{
		public string UserName { get; set; }

		public string Biography { get; set; }

		public string Gender { get; set; }

		public string Email { get; set; }

		public string DisplayName { get; set; }

		public string ProfilePictureUrl { get; set; }

		public string Token { get; set; }
	}
}
