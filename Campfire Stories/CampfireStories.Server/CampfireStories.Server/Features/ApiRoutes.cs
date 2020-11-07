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
	}
}

