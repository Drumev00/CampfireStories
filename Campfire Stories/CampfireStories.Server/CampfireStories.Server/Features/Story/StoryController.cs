namespace CampfireStories.Server.Features.Story
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Features.Common;
	using Features.Story.Models;

	using static ApiRoutes;
	using CampfireStories.Server.Infrastructure;
	using static CampfireStories.Server.Features.Common.Errors;

	public class StoryController : ApiController
	{
		private readonly IStoryService storyService;

		public StoryController(IStoryService storyService)
		{
			this.storyService = storyService;
		}

		[HttpPost]
		[Route(StoryRoutes.Create)]
		public async Task<ActionResult<ResultModel<DetailsStoryResponseModel>>> Create(CreateStoryRequestModel model)
		{
			var result = await this.storyService.CreateStoryAsync(model);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Created(nameof(Create), result);
		}

		[HttpGet]
		[Route(StoryRoutes.Details)]
		public async Task<ActionResult> GetDetailsAsync(string storyId)
		{
			var result = await this.storyService.GetDetailsAsync(storyId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result);
		}

		[HttpPut]
		[Route(StoryRoutes.Update)]
		public async Task<ActionResult> UpdateStory([FromBody] UpdateStoryRequestModel model, string storyId)
		{
			var result = await this.storyService.UpdateStoryAsync(model, storyId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpDelete]
		[Route(StoryRoutes.Delete)]
		public async Task<ActionResult> DeleteStory(string storyId, DeleteStoryRequestModel model)
		{
			var user = this.User.GetId();
			if (user != model.LoggedUser)
			{
				return Unauthorized(new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				});
			}

			var result = await this.storyService.DeleteStoryAsync(storyId, model.UserId, model.LoggedUser);
			if (!result.Success)
			{
				return Unauthorized(result.Errors);
			}

			return Ok(result.Success);
		}
	}
}
