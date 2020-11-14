namespace CampfireStories.Server.Features.Comment.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.Comment;

	public class CreateCommentRequestModel
	{
		[Required]
		[MaxLength(ContentMaxLength)]
		public string Content { get; set; }

		[Required]
		public string StoryId { get; set; }

	}
}
