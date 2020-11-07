namespace CampfireStories.Server.Features.Story.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.Story;

	public class CreateStoryRequestModel
	{
		[Required]
		[MinLength(MinTitleLength)]
		[MaxLength(MaxTitleLength)]
		public string Title { get; set; }

		[Required]
		[MinLength(MinContentLength)]
		public string Content { get; set; }

		public string PictureUrl { get; set; }

		[Required]
		public string UserId { get; set; }
	}
}
