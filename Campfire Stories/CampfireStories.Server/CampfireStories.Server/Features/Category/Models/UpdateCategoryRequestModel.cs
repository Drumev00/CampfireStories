namespace CampfireStories.Server.Features.Category.Models
{
	using System.ComponentModel.DataAnnotations;

	public class UpdateCategoryRequestModel
	{
		[Required]
		public string NewName { get; set; }
	}
}
