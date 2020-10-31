namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using CampfireStories.Server.Data.Enumerations;
	using CampfireStories.Server.Data.Models.Common;
	using Microsoft.AspNetCore.Identity;

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
			Stories = new HashSet<Story>();
		}

		public string Biography { get; set; }

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

		public virtual ICollection<Story> Stories { get; set; }
	}
}
