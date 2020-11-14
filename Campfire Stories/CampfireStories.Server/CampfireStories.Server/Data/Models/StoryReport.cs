namespace CampfireStories.Server.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	using Data.Models.Common;
	using static Data.Models.Common.Constants.StoryReport;

	public class StoryReport : BaseDeletableModel<string>
	{
		public StoryReport()
		{
			Id = Guid.NewGuid().ToString();
			IsDeleted = false;
			CreatedOn = DateTime.UtcNow;
		}

		[Required]
		public string Title { get; set; }

		[Required]
		[MinLength(MinReportContent)]
		[MaxLength(MaxReportContent)]
		public string Content { get; set; }

		public bool IsRead { get; set; }

		[Required]
		[ForeignKey(nameof(Models.User))]
		public string ReporterId { get; set; }

		public virtual User Reporter { get; set; }

		[Required]
		[ForeignKey(nameof(Models.Story))]
		public string StoryId { get; set; }

		public virtual Story Story { get; set; }
	}
}
