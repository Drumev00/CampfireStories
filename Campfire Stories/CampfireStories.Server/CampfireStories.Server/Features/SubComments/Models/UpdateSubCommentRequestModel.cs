namespace CampfireStories.Server.Features.SubComments.Models
{
	using System.ComponentModel.DataAnnotations;

	public class UpdateSubCommentRequestModel
	{
		[Required]
		public string Content { get; set; }
	}
}
