namespace CampfireStories.Server.Features.User.Models
{
	using System;


	public class UpdateUserResponseModel
	{
		public string UserName { get; set; }

		public string Email { get; set; }

		public DateTime CreatedOn { get; set; }

		public string Biography { get; set; }

		public string Gender { get; set; }

		public string DisplayName { get; set; }

		public string ProfilePictureUrl { get; set; }
	}
}
