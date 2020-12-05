namespace CampfireStories.Server.Features.SubComments
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Features.SubComments.Models;
	using Infrastructure;

	using static ApiRoutes;

	public class SubCommentsController : ApiController
	{
		private readonly ISubCommentsService subCommentsService;

		public SubCommentsController(ISubCommentsService subCommentsService)
		{
			this.subCommentsService = subCommentsService;
		}

		[HttpPost]
		[Route(SubCommentRoutes.Create)]
		public async Task<ActionResult> Create(SubCommentCreateRequestModel model)
		{
			var loggedUser = this.User.GetId();
			var result = await this.subCommentsService.CreateAsync(model, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Created(nameof(Create), result.Result);
		}

		[HttpPut]
		[Route(SubCommentRoutes.Update)]
		public async Task<ActionResult> Update(UpdateSubCommentRequestModel model, string subCommentId)
		{
			var loggedUser = this.User.GetId();
			var result = await this.subCommentsService.UpdateAsync(model, subCommentId, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpDelete]
		[Route(SubCommentRoutes.Delete)]
		public async Task<ActionResult> Delete(string subCommentId)
		{
			var loggeduser = this.User.GetId();
			var result = await this.subCommentsService.DeleteAsync(subCommentId, loggeduser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpGet]
		[Route(SubCommentRoutes.AllByRootCommentId)]
		public async Task<ActionResult> AllByRootCommentId(string rootCommentId)
		{
			var result = await this.subCommentsService.GetAllByRootCommentId(rootCommentId);

			return Ok(result);
		}

		[HttpGet]
		[Route(SubCommentRoutes.GetById)]
		public async Task<ActionResult> GetById(string subCommentId)
		{
			var result = await this.subCommentsService.GetById(subCommentId);

			return Ok(result);
		}

		[HttpGet]
		[Route(SubCommentRoutes.Like)]
		public async Task<ActionResult> Like(string subCommentId)
		{
			var result = await this.subCommentsService.Like(subCommentId);

			return Ok(result);
		}

		[HttpGet]
		[Route(SubCommentRoutes.Dislike)]
		public async Task<ActionResult> Dislike(string subCommentId)
		{
			var result = await this.subCommentsService.Dislike(subCommentId);

			return Ok(result);
		}
	}
}
