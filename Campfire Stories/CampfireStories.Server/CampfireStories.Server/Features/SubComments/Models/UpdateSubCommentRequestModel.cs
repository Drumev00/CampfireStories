namespace CampfireStories.Server.Features.SubComments.Models
{
	using System.ComponentModel.DataAnnotations;

	using static Data.Models.Common.Constants.SubComment;

	public class UpdateSubCommentRequestModel
	{
		[Required]
		[MaxLength(MaxSubCommentLength)]
		public string Content { get; set; }
	}
}
