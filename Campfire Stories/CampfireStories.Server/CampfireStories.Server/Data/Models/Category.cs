namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	
	using Data.Models.Common;
	using static Data.Models.Common.Constants.Category;

	public class Category : BaseDeletableModel<string>
	{
		public Category()
		{
			Id = Guid.NewGuid().ToString();
			IsDeleted = false;
			CreatedOn = DateTime.UtcNow;

			StoryCategories = new HashSet<StoryCategories>();
		}
		[Required]
		[MaxLength(CategoryMaxLength)]
		[MinLength(CategoryMinLength)]
		public string Name { get; set; }

		public virtual ICollection<StoryCategories> StoryCategories { get; set; }
	}
}
