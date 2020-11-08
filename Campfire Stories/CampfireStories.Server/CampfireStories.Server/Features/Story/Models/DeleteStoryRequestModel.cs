namespace CampfireStories.Server.Features.Story.Models
{
	using System.ComponentModel.DataAnnotations;

	public class DeleteStoryRequestModel
	{
		[Required]
		public string UserId { get; set; }
	}
}
