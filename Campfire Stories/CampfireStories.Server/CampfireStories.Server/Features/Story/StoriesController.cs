namespace CampfireStories.Server.Features.Story
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Infrastructure;
	using Features.Story.Models;

	using Microsoft.AspNetCore.Authorization;
	using Features.Rating.Models;
	using Features.Rating;

	using static ApiRoutes;
	using static Data.Models.Common.Constants.Story;

	public class StoriesController : ApiController
	{
		private readonly IStoriesService storyService;
		private readonly IRatingsService ratingsService;

		public StoriesController(IStoriesService storyService, IRatingsService ratingsService)
		{
			this.storyService = storyService;
			this.ratingsService = ratingsService;
		}

		[HttpPost]
		[Route(StoryRoutes.Create)]
		public async Task<ActionResult> Create(CreateStoryRequestModel model)
		{
			var loggedUser = this.User.GetId();
			var result = await this.storyService.CreateStoryAsync(model, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Created(nameof(Create), result);
		}

		[HttpGet]
		[AllowAnonymous]
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

		[HttpPost]
		[Route(StoryRoutes.Rate)]
		public async Task<ActionResult> Rate(RateStoryRequestModel model)
		{
			var loggedUser = this.User.GetId();
			var result = await this.ratingsService.Rate(model.StoryId, loggedUser, model.Rating);

			return Ok(result);
		}

		[HttpGet]
		[Route(StoryRoutes.Rated)]
		public async Task<ActionResult> Rated(string storyId)
		{
			var loggedUser = this.User.GetId();
			var result = await this.ratingsService.AlreadyRated(storyId, loggedUser);

			return Ok(result);
		}

		[HttpGet]
		[AllowAnonymous]
		[Route(StoryRoutes.GetAll)]
		public async Task<ActionResult> GetAllStories(string title, int page)
		{
			var result = await this.storyService.GetAll(StoriesPerPage, (page - 1) * StoriesPerPage, title);
			result.CurrentPage = page;

			return Ok(result);
		}

		[HttpGet]
		[Route(StoryRoutes.ById)]
		public async Task<ActionResult> GetAllById(string userId)
		{
			var result = await this.storyService.GetAllByUserId(userId);

			return Ok(result);
		}

		[HttpGet]
		[Route(StoryRoutes.Foreign)]
		public async Task<ActionResult> GetAllForeignStories(string username)
		{
			var result = await this.storyService.GetAllByForeignUsername(username);

			return Ok(result);
		}


		[HttpPut]
		[Route(StoryRoutes.Update)]
		public async Task<ActionResult> UpdateStory(string storyId, UpdateStoryRequestModel model)
		{
			var loggedUser = this.User.GetId();
			var result = await this.storyService.UpdateStoryAsync(model, storyId, loggedUser);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpDelete]
		[Route(StoryRoutes.Delete)]
		public async Task<ActionResult> DeleteStory(string storyId)
		{
			var loggedUser = this.User.GetId();
			var result = await this.storyService.DeleteStoryAsync(storyId, loggedUser);
			if (!result.Success)
			{
				return Unauthorized(result.Errors);
			}

			return Ok(result.Success);
		}
	}
}
