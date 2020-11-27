namespace CampfireStories.Server.Features.User.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.User;
	public class UpdateUserRequestModel
	{
		[MinLength(MinBiographyLength)]
		public string Biography { get; set; }

		[MinLength(MinDisplayNameLength)]
		[MaxLength(MaxDisplayNameLength)]
		public string DisplayName { get; set; }

		[Required]
		public string Email { get; set; }

		public string ProfilePictureUrl { get; set; }
	}
}
