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

		[HttpPut]
		[Route(CategoryRoutes.Update)]
		public async Task<ActionResult> Update(UpdateCategoryRequestModel model)
		{
			var userId = this.User.GetId();
			var result = await this.categoryService.UpdateCategoryAsync(model.NewName, model.CategoryId, userId);
			if (!result.Success)
			{
				return Unauthorized(result.Errors);
			}

			return this.Ok(result.Result);
		}

		[HttpDelete]
		[Route(CategoryRoutes.Delete)]
		public async Task<ActionResult<ResultModel<bool>>> Delete([FromBody] CategoryDeleteModel model)
		{
			var userId = this.User.GetId();
			var result = await this.categoryService.DeleteCategoryAsync(model.Id, userId);
			if (!result.Success)
			{
				return this.Unauthorized(Errors.CategoryErrors.NoPermissionToCreateCategory);
			}

			return Ok(result.Result);
		}
	}
}
