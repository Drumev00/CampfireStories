namespace CampfireStories.Server.Features.Story.Models
{
	using System;

	public class DetailsStoryResponseModel
	{
		public string Title { get; set; }

		public string Content { get; set; }

		public double Rating { get; set; }

		public int Votes { get; set; }

		public string PictureUrl { get; set; }

		public string Username { get; set; }

		public DateTime CreatedOn { get; set; }

		public string[] Categories { get; set; }
	}
}
