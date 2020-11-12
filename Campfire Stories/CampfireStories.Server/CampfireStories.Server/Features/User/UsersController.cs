namespace CampfireStories.Server.Features.User
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	using Features.Common;
	using Infrastructure;
	using Features.User.Models;

	using static Features.Common.Errors;
	using static ApiRoutes;

	public class UsersController : ApiController
	{
		private readonly IUsersService userService;

		public UsersController(IUsersService userService)
		{
			this.userService = userService;
		}


		[HttpGet]
		[Route(UserRoutes.Profile)]
		public async Task<ActionResult> Profile(string userId)
		{
			var result = await this.userService.GetProfile(userId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpPut]
		[Route(UserRoutes.Update)]
		public async Task<ActionResult> UpdateUser(UpdateUserRequestModel model)
		{
			var loggedUserId = this.User.GetId();
			if (loggedUserId != model.UserId)
			{
				return BadRequest( new ResultModel<UpdateUserResponseModel>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				});
			}

			var result = await this.userService.UpdateUser(model);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result.Result);
		}

		[HttpDelete]
		[Route(UserRoutes.Delete)]
		public async Task<ActionResult<ResultModel<bool>>> DeleteUser(DeleteUserModel model)
		{
			var loggedUser = this.User.GetId();
			if (loggedUser != model.UserId)
			{
				return BadRequest(new ResultModel<bool>
				{
					Errors = { UserErrors.UserHaveNoPermissionToUpdate }
				});
			}
			var result = await this.userService.DeleteUser(model.UserId);
			if (!result.Success)
			{
				return BadRequest(result.Errors);
			}

			return Ok(result);
		}
	}
}
