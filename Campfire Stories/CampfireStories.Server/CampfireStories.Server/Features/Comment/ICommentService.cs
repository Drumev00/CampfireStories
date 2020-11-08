namespace CampfireStories.Server.Features.Comment
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Features.Comment.Models;
	using Features.Common;

	public interface ICommentService
	{
		Task<ResultModel<CreateCommentResponseModel>> CreateCommentAsync(CreateCommentRequestModel model);

		Task<IEnumerable<CreateCommentResponseModel>> GetAllByStoryId(string storyId);

		Task<ResultModel<bool>> UpdateCommentAsync(UpdateCommentRequestModel model, string commentId, string loggedUser, bool isAdmin);

		Task<ResultModel<bool>> DeleteCommentAsync(string commentId, string userId);
	}
}
