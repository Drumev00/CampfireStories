﻿namespace CampfireStories.Server.Features.Identity.Models
{
	using System.ComponentModel.DataAnnotations;


	public class RegisterUserRequestModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string Gender { get; set; }

		[Required]
		public string Email { get; set; }

	}
}
