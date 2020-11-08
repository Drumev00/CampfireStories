namespace CampfireStories.Server.Features.Comment
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Infrastructure;
	using Features.Comment.Models;

	using static Features.Common.Errors;
	using static ApiRoutes;
	using CampfireStories.Server.Features.User;

	public class CommentController : ApiController
	{
		private readonly ICommentService commentService;
		private readonly IUserService userService;

		public CommentController(ICommentService commentService, IUserService userService)
		{
			this.commentService = commentService;
			this.userService = userService;
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
			var isAdmin = await this.userService.IsAdminAsync(model.UserId);
			
			var result = await this.commentService.UpdateCommentAsync(model, commentId, loggedUser, isAdmin);
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
	}
}
