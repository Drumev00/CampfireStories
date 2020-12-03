namespace CampfireStories.Server.Features
{
	public static class ApiRoutes
	{
		public static class IdentityRoutes
		{
			public const string IdentityRoute = "/identity";
			public const string Register = IdentityRoute + "/register";
			public const string Login = IdentityRoute + "/login";
		}

		public static class CategoryRoutes
		{
			public const string CategoryRoute = "/category";
			public const string GetAll = CategoryRoute;
			public const string Create = CategoryRoute;
			public const string Details = CategoryRoute + "/{categoryId}";
			public const string Update = CategoryRoute + "/{categoryId}";
			public const string Delete = CategoryRoute + "/{categoryId}";
		}

		public static class UserRoutes
		{
			public const string UserRoute = "/user";
			public const string AdminRoute = "/admin";

			public const string Update = UserRoute + "/{userId}";
			public const string Delete = UserRoute + "/{userId}";
			public const string Profile = UserRoute + "/{userId}";
			public const string ResetPhoto = UserRoute + "/reset";
			public const string Ban = AdminRoute + UserRoute + "/{userId}";
			public const string Unban = AdminRoute + UserRoute + "/{userId}";
		}

		public static class StoryRoutes
		{
			public const string StoryRoute = "/story";
			public const string Create = StoryRoute;
			public const string GetAll = StoryRoute;
			public const string Details = StoryRoute + "/{storyId}";
			public const string Update = StoryRoute + "/{storyId}";
			public const string Delete = StoryRoute + "/{storyId}";
			public const string Rate = StoryRoute + "/rate";
			public const string ById = StoryRoute + "/myStories/{userId}";
			public const string Foreign = StoryRoute + "/foreign/{username}";

		}

		public static class CommentRoutes
		{
			public const string CommentRoute = "/comment";
			public const string Create = CommentRoute;
			public const string Update = CommentRoute + "/{commentId}";
			public const string Delete = CommentRoute + "/{commentId}";
			public const string All = CommentRoute + "/{storyId}";
			public const string Like = CommentRoute + "/like/{commentId}";
			public const string Dislike = CommentRoute + "/dislike/{commentId}";
			public const string ById = CommentRoute + "/getOne/{commentId}";

		}

		public static class SubCommentRoutes
		{
			public const string SubCommentRoute = "/subComment";
			public const string Create = SubCommentRoute;
			public const string Update = SubCommentRoute + "/{subCommentId}";
			public const string Delete = SubCommentRoute + "/{subCommentId}";
		}

		public static class StoryReportRoutes
		{
			public const string StoryReportRoute = "/storyReport";
			public const string Create = StoryReportRoute;
			public const string AllReadOrNot = StoryReportRoute;
			public const string GetDetailsById = StoryReportRoute + "/{storyReportId}";
			public const string Update = StoryReportRoute + "/{storyReportId}";
			public const string Delete = StoryReportRoute + "/{storyReportId}";
			public const string AdminListing = "/admin" + StoryReportRoute;

		}

		public static class UserReportRoutes
		{
			public const string UserReportRoute = "/userReport";
			public const string Create = UserReportRoute;
			public const string AllByRead = UserReportRoute;
			public const string Details = UserReportRoute + "/{userReportId}";
			public const string Update = UserReportRoute + "/{userReportId}";
			public const string Delete = UserReportRoute + "/{userReportId}";
			public const string AdminListing = "/admin" + UserReportRoute;
		}
	}
}

