namespace CampfireStories.Server.Features.Category.Models
{
	using System.ComponentModel.DataAnnotations;

	public class CategoryListingModel
	{
		[Required]
		public string CategoryId { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
