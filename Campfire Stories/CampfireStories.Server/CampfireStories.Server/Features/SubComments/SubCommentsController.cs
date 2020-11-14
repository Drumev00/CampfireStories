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
	}
}
