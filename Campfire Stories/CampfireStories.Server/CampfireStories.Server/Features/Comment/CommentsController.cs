namespace CampfireStories.Server.Features.Comment
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

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
			var loggedUser = this.User.GetId();
			var result = await this.commentService.CreateCommentAsync(model, loggedUser);
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
			var result = await this.commentService.UpdateCommentAsync(model.Content, commentId, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
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

		[HttpGet]
		[Route(CommentRoutes.Like)]
		public async Task<ActionResult> Like(string commentId)
		{
			var result = await this.commentService.Like(commentId);

			return Ok(result);
		}

		[HttpGet]
		[Route(CommentRoutes.Dislike)]
		public async Task<ActionResult> Dislike(string commentId)
		{
			var result = await this.commentService.Dislike(commentId);

			return Ok(result);
		}

		[HttpGet]
		[Route(CommentRoutes.ById)]
		public async Task<ActionResult> GetById(string commentId)
		{
			var result = await this.commentService.GetById(commentId);

			return Ok(result);
		}

	}
}
