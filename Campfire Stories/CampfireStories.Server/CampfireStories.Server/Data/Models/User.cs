namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using Microsoft.AspNetCore.Identity;

	using Data.Enumerations;
	using Data.Models.Common;
	using static Data.Models.Common.Constants.User;

	public class User : IdentityUser, IAuditInfo, IDeletableEntity
	{
		public User()
		{
			Id = Guid.NewGuid().ToString();
			IsDeleted = false;
			CreatedOn = DateTime.UtcNow;

			Comments = new HashSet<Comment>();
			SubComments = new HashSet<SubComment>();
			StoryReports = new HashSet<StoryReport>();
			UserReports = new HashSet<UserReport>();
			Stories = new HashSet<Story>();
			Ratings = new HashSet<Rating>();
		}

		[MinLength(MinBiographyLength)]
		public string Biography { get; set; }

		[MinLength(MinDisplayNameLength)]
		[MaxLength(MaxDisplayNameLength)]
		public string DisplayName { get; set; }

		public string ProfilePictureUrl { get; set; }

		[Required]
		public Gender Gender { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<SubComment> SubComments { get; set; }

		public virtual ICollection<StoryReport> StoryReports { get; set; }

		public virtual ICollection<UserReport> UserReports { get; set; }

		public virtual ICollection<Story> Stories { get; set; }

		public virtual ICollection<Rating> Ratings { get; set; }
	}
}
