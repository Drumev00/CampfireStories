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

			public const int MinRating = 0;
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

		public static class Roles
		{
			public const string AdministratorRoleName = "Administrator";
			public const string RegularUserRoleName = "User";
			public const string BannedUserRoleName = "Banned";
		}

		public static class AdminCredentials
		{
			public const string AdminUsername = "Alex_Drumev";
			public const string AdminEmail = "alex@mail.com";
			public const string AdminPassword = "123123strong";
			public const string AdminGender = "1";
		}
	}
}
