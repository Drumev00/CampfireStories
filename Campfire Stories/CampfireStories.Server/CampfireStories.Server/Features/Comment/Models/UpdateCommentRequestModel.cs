namespace CampfireStories.Server.Features.Comment.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.Comment;

	public class UpdateCommentRequestModel
	{
		[Required]
		[MaxLength(ContentMaxLength)]
		public string Content { get; set; }
	}
}
