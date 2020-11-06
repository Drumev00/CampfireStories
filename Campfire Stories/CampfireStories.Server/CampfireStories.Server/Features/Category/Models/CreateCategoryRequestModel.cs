namespace CampfireStories.Server.Features.Category.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.Category;

	public class CreateCategoryRequestModel
	{
		[Required]
		[MaxLength(CategoryMaxLength)]
		[MinLength(CategoryMinLength)]
		public string Name { get; set; }
	}
}
