namespace CampfireStories.Server.Features.Common
{
	public static class Errors
	{
		public static class Identity
		{
			public const string UserNotFound = "User {0} not found.";
			public const string PasswordsDontMatch = "Passwords don't match!";
			public const string InvalidUser = "User\'s Id shouldn\'t be null or whitespace!";
			public const string UserAlreadyExists = "User with username {0} aldeady exists.";
		}

		public static class CategoryErrors
		{
			public const string CategoryNameMustHaveValue = "Category name musn't be null or whitespace!";
			public const string NoPermissionToCreateCategory = "You\'re unauthorized to do this.";
		}

		public static class UserErrors 
		{
			public const string InvalidUserId = "There\'s no user with such Id or it is deleted.";
			public const string UserHaveNoPermissionToUpdate = "You can modify only your user data.";
			public const string BannedUserCreateStory = "You\'re banned. You have no permission to write a story";

		}

		public static class StoryErrors
		{
			public const string NotFoundOrDeletedStory = "There\'s no such story or it\'s deleted.";

		}
	}
}
