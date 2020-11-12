namespace CampfireStories.Server.Features.Comment.Models
{
	using CampfireStories.Server.Features.SubComments.Models;
	using System;
	using System.Collections.Generic;

	public class CreateCommentResponseModel
	{
		public string Id { get; set; }

		public DateTime CreatedOn { get; set; }

		public string Content { get; set; }

		public int Likes { get; set; }

		public int Dislikes { get; set; }

		public UserCommentDetailsModel User { get; set; }

		public IEnumerable<SubCommentListingResponseModel> SubComments { get; set; }
	}
}
