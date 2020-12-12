namespace CampfireStories.Server.Features.Story.Models
{
	using System.Collections.Generic;

	public class ListingPaginationStories
	{
		public int TotalItems { get; set; }

		public IEnumerable<IndividualStory> Stories { get; set; }

		public int TotalPages { get; set; }

		public int CurrentPage { get; set; }
	}
}
