namespace CampfireStories.Server.Features.SubComments.Models
{
	using System.ComponentModel.DataAnnotations;
	using static Data.Models.Common.Constants.SubComment;

	public class SubCommentCreateRequestModel
	{
		[Required]
		[MaxLength(MaxSubCommentLength)]
		public string Content { get; set; }

		[Required]
		public string RootCommentId { get; set; }
	}
}
