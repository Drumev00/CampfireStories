namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using CampfireStories.Server.Data.Models.Common;

	public class Story : BaseDeletableModel<string>
	{
		public Story()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
			IsDeleted = false;

			StoryCategories = new HashSet<StoryCategories>();
			StoryReports = new HashSet<StoryReport>();
			Comments = new HashSet<Comment>();
			Rating = 1.0;
		}

		[Required]
		[MinLength(1)]
		[MaxLength(80)]
		public string Title { get; set; }

		[Required]
		[MinLength(500)]
		public string Content { get; set; }

		[Range(1, 10)]
		public double Rating { get; set; }

		public int Votes { get; set; }

		[Required]
		[ForeignKey(nameof(Models.User))]
		public string UserId { get; set; }

		public virtual User User { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<StoryReport> StoryReports { get; set; }

		public virtual ICollection<StoryCategories> StoryCategories { get; set; }
	}
}
