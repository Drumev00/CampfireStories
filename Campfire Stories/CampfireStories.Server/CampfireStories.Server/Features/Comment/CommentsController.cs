namespace CampfireStories.Server.Features.Comment
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Features.User;
	using Infrastructure;
	using Features.Comment.Models;

	using static ApiRoutes;

	public class CommentsController : ApiController
	{
		private readonly ICommentsService commentService;

		public CommentsController(ICommentsService commentService)
		{
			this.commentService = commentService;
		}

		[HttpPost]
		[Route(CommentRoutes.Create)]
		public async Task<ActionResult> Create(CreateCommentRequestModel model)
		{
			var result = await this.commentService.CreateCommentAsync(model);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Created(nameof(Create), result.Result);
		}

		[HttpPut]
		[Route(CommentRoutes.Update)]
		public async Task<ActionResult> Update(UpdateCommentRequestModel model, string commentId)
		{
			var loggedUser = this.User.GetId();
			
			var result = await this.commentService.UpdateCommentAsync(model, commentId, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Success);
		}

		[HttpDelete]
		[Route(CommentRoutes.Delete)]
		public async Task<ActionResult> Delete(string commentId)
		{
			var loggedUser = this.User.GetId();
			var result = await this.commentService.DeleteCommentAsync(commentId, loggedUser);
			if (!result.Success)
			{
				return Unauthorized(result.Errors);
			}

			return Ok(result.Success);
		}

		[HttpGet]
		[Route(CommentRoutes.All)]
		public async Task<ActionResult> All(string storyId)
		{
			var result = await this.commentService.GetAllByStoryId(storyId);
			
			return Ok(result);
		}
	}
}
