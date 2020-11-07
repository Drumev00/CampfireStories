namespace CampfireStories.Server.Features.Story.Models
{
	using System.ComponentModel.DataAnnotations;

	public class DetailsStoryRequestModel
	{
		[Required]
		public string StoryId { get; set; }
	}
}
