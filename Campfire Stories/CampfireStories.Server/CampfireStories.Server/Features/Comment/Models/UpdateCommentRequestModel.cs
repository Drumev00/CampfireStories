﻿namespace CampfireStories.Server.Features.Comment.Models
{
	using System.ComponentModel.DataAnnotations;

	public class UpdateCommentRequestModel
	{
		[Required]
		public string Content { get; set; }
	}
}
