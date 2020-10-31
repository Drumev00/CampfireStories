namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	
	using CampfireStories.Server.Data.Models.Common;

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
		[MaxLength(40)]
		[MinLength(3)]
		public string Name { get; set; }

		public virtual ICollection<StoryCategories> StoryCategories { get; set; }
	}
}
