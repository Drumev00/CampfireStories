namespace CampfireStories.Server.Features.SubComments.Models
{
	using System;

	using Features.Comment.Models;

	public class SubCommentCreateResponseModel
	{
		public DateTime CreatedOn { get; set; }

		public string Content { get; set; }

		public int Likes { get; set; }

		public int Dislikes { get; set; }

		public UserCommentDetailsModel User { get; set; }
	}
}
