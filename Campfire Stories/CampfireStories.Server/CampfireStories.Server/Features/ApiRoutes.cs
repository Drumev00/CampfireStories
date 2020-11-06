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
			public const string Create = CategoryRoute + "/create";
			public const string Update = CategoryRoute + "/update";
			public const string Delete = CategoryRoute + "/delete";
		}
	}
}

