namespace CampfireStories.Server.Data.Models
{
	using System;
	using Data.Models.Common;

	public class StoryCategories : BaseDeletableModel<string>
	{
		public StoryCategories()
		{
			Id = Guid.NewGuid().ToString();
			IsDeleted = false;
			CreatedOn = DateTime.UtcNow;
		}

		public string StoryId { get; set; }

		public virtual Story Story { get; set; }

		public string CategoryId { get; set; }

		public virtual Category Category { get; set; }
	}
}
