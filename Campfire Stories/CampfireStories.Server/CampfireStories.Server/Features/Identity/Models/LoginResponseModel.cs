namespace CampfireStories.Server.Features.Identity.Models
{
	public class LoginResponseModel
	{
		public string ProfilePictureUrl { get; set; }

		public string UserId { get; set; }

		public string DisplayName { get; set; }

		public string UserName { get; set; }

		public string Token { get; set; }

		public bool IsAdmin { get; set; }
	}
}
