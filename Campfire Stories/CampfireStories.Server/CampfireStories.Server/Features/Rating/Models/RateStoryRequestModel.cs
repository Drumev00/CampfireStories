namespace CampfireStories.Server.Features.Rating.Models
{
	using System.ComponentModel.DataAnnotations;

	public class RateStoryRequestModel
	{
		[Required]
		public string UserId { get; set; }

		[Required]
		public string StoryId { get; set; }

		[Required]
		public int Rating { get; set; }
	}
}
