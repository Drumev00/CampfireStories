namespace CampfireStories.Server.Features.Category.Models
{
	using System.ComponentModel.DataAnnotations;


	public class CategoryDeleteModel
	{
		[Required]
		public string Id { get; set; }
	}
}
