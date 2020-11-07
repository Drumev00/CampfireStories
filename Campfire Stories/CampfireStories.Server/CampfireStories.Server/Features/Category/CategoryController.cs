namespace CampfireStories.Server.Features.Category
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Features.Category.Models;
	using Features.Common;
	using Infrastructure;

	using static Features.ApiRoutes;

	public class CategoryController : ApiController
	{
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		[HttpPost]
		[Route(CategoryRoutes.Create)]
		public async Task<ActionResult<string>> Create(CreateCategoryRequestModel model)
		{
			var userId = this.User.GetId();
			var result = await this.categoryService.CreateCategoryAsync(model.Name, userId);
			if (!result.Success)
			{
				return Unauthorized(new { result.Errors });
			};

			return Created(nameof(Create), result);
		}

		[HttpPut]
		[Route(CategoryRoutes.Update)]
		public async Task<ActionResult> Update(UpdateCategoryRequestModel model)
		{
			var userId = this.User.GetId();
			var result = await this.categoryService.UpdateCategoryAsync(model.NewName, model.CategoryId, userId);
			if (!result.Success)
			{
				return Unauthorized(new { result.Errors });
			}

			return this.Ok();
		}

		[HttpDelete]
		[Route(CategoryRoutes.Delete)]
		public async Task<ActionResult<ResultModel<bool>>> Delete([FromBody] CategoryDeleteModel model)
		{
			var userId = this.User.GetId();
			var attempt = await this.categoryService.DeleteCategoryAsync(model.Id, userId);
			if (!attempt.Success)
			{
				return this.Unauthorized(new ResultModel<bool>
				{
					Errors = { Errors.CategoryErrors.NoPermissionToCreateCategory },
				});
			}

			return Ok();
		}
	}
}
