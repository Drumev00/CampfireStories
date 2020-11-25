namespace CampfireStories.Server.Features.Category
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Features.Category.Models;
	using Features.Common;
	using Infrastructure;

	using static Features.ApiRoutes;

	public class CategoriesController : ApiController
	{
		private readonly ICategoriesService categoryService;

		public CategoriesController(ICategoriesService categoryService)
		{
			this.categoryService = categoryService;
		}

		[HttpPost]
		[Route(CategoryRoutes.Create)]
		public async Task<ActionResult> Create(CreateCategoryRequestModel model)
		{
			var userId = this.User.GetId();
			var result = await this.categoryService.CreateCategoryAsync(model.Name, userId);
			if (!result.Success)
			{
				return Unauthorized(new { result.Errors });
			};

			return Created(nameof(Create), result);
		}

		[HttpGet]
		[Route(CategoryRoutes.GetAll)]
		public async Task<ActionResult> GetAll()
		{
			var result = await this.categoryService.GetAll();

			return Ok(result);
		}

		[HttpGet]
		[Route(CategoryRoutes.Details)]
		public async Task<ActionResult> Details(string categoryId)
		{
			var result = await this.categoryService.GetDetails(categoryId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpPut]
		[Route(CategoryRoutes.Update)]
		public async Task<ActionResult> Update(string categoryId, UpdateCategoryRequestModel model)
		{
			var userId = this.User.GetId();
			var result = await this.categoryService.UpdateCategoryAsync(model.NewName, categoryId, userId);
			if (!result.Success)
			{
				return Unauthorized(result.Errors);
			}

			return this.Ok(result.Result);
		}

		[HttpDelete]
		[Route(CategoryRoutes.Delete)]
		public async Task<ActionResult<ResultModel<bool>>> Delete(string categoryId)
		{
			var userId = this.User.GetId();
			var result = await this.categoryService.DeleteCategoryAsync(categoryId, userId);
			if (!result.Success)
			{
				return this.Unauthorized(Errors.CategoryErrors.NoPermissionToCreateCategory);
			}

			return Ok(result.Result);
		}
	}
}
