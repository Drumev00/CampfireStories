namespace CampfireStories.Server.Features.Comment.Models
{
	using System.ComponentModel.DataAnnotations;

	public class UpdateCommentRequestModel
	{
		[Required]
		public string UserId { get; set; }

		[Required]
		public string Content { get; set; }
	}
}
