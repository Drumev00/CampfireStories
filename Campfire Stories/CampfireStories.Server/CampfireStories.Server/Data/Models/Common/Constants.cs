namespace CampfireStories.Server.Data.Models.Common
{
	public static class Constants
	{
		public static class Category
		{
			public const int CategoryMinLength = 3;
			public const int CategoryMaxLength = 40;
		}

		public static class Comment
		{
			public const int ContentMaxLength = 500;
		}

		public static class Story
		{
			public const int MinTitleLength = 1;
			public const int MaxTitleLength = 80;

			public const int MinContentLength = 500;

			public const int MinRating = 1;
			public const int MaxRating = 10;
		}

		public static class StoryReport
		{
			public const int MinReportContent = 10;
			public const int MaxReportContent = 500;
		}

		public static class SubComment
		{
			public const int MaxSubCommentLength = 500;
		}

		public static class User
		{
			public const int MinBiographyLength = 20;

			public const int MinDisplayNameLength = 2;
			public const int MaxDisplayNameLength = 50;
		}

		public static class UserReport
		{
			public const int MinUserReportContent = 10;
			public const int MaxUserReportContent = 500;
		}
	}
}
