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

		Task<ResultModel<string>> UpdateCommentAsync(string content, string commentId, string loggedUser);

		Task<ResultModel<bool>> DeleteCommentAsync(string commentId, string userId);

		Task<int> Like(string commentId);

		Task<int> Dislike(string commentId);

		Task<CreateCommentResponseModel> GetById(string commentId);
	}
}
