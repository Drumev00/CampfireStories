namespace CampfireStories.Server.Features.Rating
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;

	using Features.Rating.Models;
	using Features.Common;
	using Data.Models;
	using Data;

	using static Features.Common.Errors;

	public class RatingsService : IRatingsService
	{
		private readonly CampfireStoriesDbContext dbContext;

		public RatingsService(CampfireStoriesDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<bool> AlreadyRated(string storyId, string userId)
		{
			var rating = await this.dbContext
				.Ratings
				.Where(s => s.StoryId == storyId && s.UserId == userId)
				.FirstOrDefaultAsync();

			if (rating == null)
			{
				return false;
			}

			return true;
		}

		public async Task<RateStoryResponseModel> Rate(string storyId, string userId, int rating)
		{
			var story = await this.dbContext
				.Stories
				.Where(s => s.Id == storyId)
				.FirstOrDefaultAsync();

			double actualRating = 0.0;
			if (story.Rating == 0 && story.Votes == 0)
			{
				actualRating = rating;
			}
			else
			{
				var total = (double)(story.Rating * story.Votes);
				total += rating;
				actualRating = (double)(total / (story.Votes + 1));
			}

			story.Rating = Math.Round(actualRating, 2);
			story.Votes++;

			this.dbContext.Stories.Update(story);

			var databaseEntity = new Rating
			{
				UserId = userId,
				StoryId = storyId,
				CreatedOn = DateTime.UtcNow,
			};

			this.dbContext.Ratings.Add(databaseEntity);

			await this.dbContext.SaveChangesAsync();

			return new RateStoryResponseModel
			{
				UserId = story.UserId,
				StoryId = story.Id,
				Rating = story.Rating,
				Votes = story.Votes,
			};
		}
	}
}
