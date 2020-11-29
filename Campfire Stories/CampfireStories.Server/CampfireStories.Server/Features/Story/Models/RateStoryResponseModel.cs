namespace CampfireStories.Server.Features.Story.Models
{
	public class RateStoryResponseModel
	{
		public string UserId { get; set; }

		public string StoryId { get; set; }

		public double Rating { get; set; }

		public int Votes { get; set; }
	}
}
