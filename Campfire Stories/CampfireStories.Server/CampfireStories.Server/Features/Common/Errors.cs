namespace CampfireStories.Server.Features.Common
{
	public static class Errors
	{
		public static class Identity
		{
			public const string UserNotFound = "User {0} not found.";
			public const string PasswordsDontMatch = "Passwords don't match!";
			public const string UserAlreadyExists = "User with username {0} aldeady exists.";
		}
	}
}
