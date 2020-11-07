namespace CampfireStories.Server.Features.StoryCategories.Models
{
	using System.ComponentModel.DataAnnotations;

	public class UpdateStoryCategoriesRequestModel
	{
		[Required]
		public string StoryId { get; set; }

		public string[] Categories { get; set; }
	}
}
