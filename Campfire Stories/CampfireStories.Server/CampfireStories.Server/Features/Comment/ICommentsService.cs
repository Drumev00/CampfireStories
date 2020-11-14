namespace CampfireStories.Server.Features.Comment
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Features.Comment.Models;
	using Features.Common;

	public interface ICommentsService
	{
		Task<ResultModel<string>> CreateCommentAsync(CreateCommentRequestModel model, string userId);

		Task<IEnumerable<CreateCommentResponseModel>> GetAllByStoryId(string storyId);

		Task<ResultModel<bool>> UpdateCommentAsync(string content, string commentId, string loggedUser);

		Task<ResultModel<bool>> DeleteCommentAsync(string commentId, string userId);
	}
}
