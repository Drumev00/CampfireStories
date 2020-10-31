namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	using CampfireStories.Server.Data.Models.Common;

	public class SubComment : BaseDeletableModel<string>
	{
		public SubComment()
		{
			Id = Guid.NewGuid().ToString();
			IsDeleted = false;
			CreatedOn = DateTime.UtcNow;
		}

		[Required]
		[MaxLength(500)]
		public string Content { get; set; }

		[Required]
		[ForeignKey(nameof(Models.User))]
		public string UserId { get; set; }

		public virtual User User { get; set; }

		[Required]
		[ForeignKey(nameof(Models.Comment))]
		public string RootCommentId { get; set; }

		public virtual Comment Comment { get; set; }
	}
}
