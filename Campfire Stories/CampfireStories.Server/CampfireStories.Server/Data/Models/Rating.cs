namespace CampfireStories.Server.Data.Models
{
	using Data.Models.Common;
	using System;

	public class Rating : BaseModel<string>
	{
		public Rating()
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
		}

		public string StoryId { get; set; }

		public virtual Story Story { get; set; }

		public string UserId { get; set; }

		public virtual User User { get; set; }
	}
}
