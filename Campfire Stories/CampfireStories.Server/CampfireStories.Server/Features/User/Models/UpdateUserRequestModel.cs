namespace CampfireStories.Server.Features.User.Models
{
	using System;

	public class UpdateUserRequestModel
	{
		public string Biography { get; set; }

		public string Gender { get; set; }

		public string DisplayName { get; set; }

		public string ProfilePictureUrl { get; set; }

		public string UserId { get; set; }
	}
}
