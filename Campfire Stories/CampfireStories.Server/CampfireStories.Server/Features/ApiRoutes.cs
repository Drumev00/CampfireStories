﻿namespace CampfireStories.Server.Features
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
			public const string Create = CategoryRoute + "/{categoryId}";
			public const string Update = CategoryRoute + "/{categoryId}";
			public const string Delete = CategoryRoute + "/{categoryId}";
		}

		public static class UserRoutes
		{
			public const string UserRoute = "/user";
			public const string Update = UserRoute + "/{userId}";
			public const string Delete = UserRoute + "/{userId}";
			public const string Profile = UserRoute + "/{userId}";
		}

		public static class StoryRoutes
		{
			public const string StoryRoute = "/story";
			public const string Create = StoryRoute;
			public const string Details = StoryRoute + "/{storyId}";
			public const string Update = StoryRoute + "/{storyId}";
			public const string Delete = StoryRoute + "/{storyId}";
		}

		public static class CommentRoutes
		{
			public const string CommentRoute = "/comment";
			public const string Create = CommentRoute;
			public const string Update = CommentRoute + "/{commentId}";
			public const string Delete = CommentRoute + "/{commentId}";
			public const string All = CommentRoute + "/{storyId}";

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
		}
	}
}

