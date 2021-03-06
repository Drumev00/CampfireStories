﻿namespace CampfireStories.Server.Features.SubComments
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Features.Common;
	using Features.SubComments.Models;

	public interface ISubCommentsService
	{
		Task<ResultModel<string>> CreateAsync(SubCommentCreateRequestModel model, string userId);

		Task<IEnumerable<SubCommentListingResponseModel>> GetAllByRootCommentId(string rootCommentId);

		Task<ResultModel<string>> UpdateAsync(UpdateSubCommentRequestModel model, string subCommentId, string userId);

		Task<ResultModel<bool>> DeleteAsync(string subCommentId, string userId);

		Task<ResultModel<bool>> DeleteAllByRootCommentIdAsync(string rootCommentId);

		Task<IndividualSubCommentResponseModel> GetById(string subCommentId);

		Task<bool> Like(string subCommentId);

		Task<bool> Dislike(string subCommentId);
	}
}
