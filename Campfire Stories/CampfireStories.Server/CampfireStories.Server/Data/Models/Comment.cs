namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	using CampfireStories.Server.Data.Models.Common;

	public class Comment : BaseDeletableModel<string>
	{
		public Comment()
		{
			Id = Guid.NewGuid().ToString();
			IsDeleted = false;
			CreatedOn = DateTime.UtcNow;

			SubComments = new HashSet<SubComment>();
		}

		[Required]
		[MaxLength(500)]
		public string Content { get; set; }

		public int Likes { get; set; }

		public int Dislikes { get; set; }

		[Required]
		[ForeignKey(nameof(Models.User))]
		public string UserId { get; set; }

		public virtual User User { get; set; }

		[Required]
		[ForeignKey(nameof(Models.Story))]
		public string StoryId { get; set; }

		public virtual Story Story { get; set; }

		public virtual ICollection<SubComment> SubComments { get; set; }
	}
}
