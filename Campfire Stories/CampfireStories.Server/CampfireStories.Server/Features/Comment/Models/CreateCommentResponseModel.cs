namespace CampfireStories.Server.Features.Comment.Models
{
	using System;


	public class CreateCommentResponseModel
	{
		public DateTime CreatedOn { get; set; }

		public string Content { get; set; }

		public int Likes { get; set; }

		public int Dislikes { get; set; }

		public UserCommentDetailsModel User { get; set; }
	}
}
