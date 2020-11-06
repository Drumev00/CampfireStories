namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	using Data.Models.Common;
	using static Data.Models.Common.Constants.Story;

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
		[MinLength(MinTitleLength)]
		[MaxLength(MaxTitleLength)]
		public string Title { get; set; }

		[Required]
		[MinLength(MinContentLength)]
		public string Content { get; set; }

		[Range(MinRating, MaxRating)]
		public double Rating { get; set; }

		public int Votes { get; set; }

		public string PictureUrl { get; set; }


		[Required]
		[ForeignKey(nameof(Models.User))]
		public string UserId { get; set; }

		public virtual User User { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<StoryReport> StoryReports { get; set; }

		public virtual ICollection<StoryCategories> StoryCategories { get; set; }
	}
}
