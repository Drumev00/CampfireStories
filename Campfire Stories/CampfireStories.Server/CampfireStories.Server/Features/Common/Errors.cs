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
			public const string UserHaveNoPermissionToBan = "Only administrators can ban users.";
		}

		public static class StoryErrors
		{
			public const string NotFoundOrDeletedStory = "There\'s no such story or it\'s deleted.";
		}

		public static class CommentErrors
		{
			public const string BannedUserCreateComment = "You\'re banned. You have no permission to write comments.";
			public const string NotFoundOrDeletedComment = "There\'s no such comment or it\'s deleted.";
			public const string UserHaveNoPermissionToDeleteComments = "You have no permission to delete comments!";
		}

		public static class SubCommentErrors
		{
			public const string BannedUserCreateSubComment = "You\'re banned. You have no permission to write comments.";
			public const string NotFoundOrDeletedSubComment = "There\'s no such subcomment or it\'s deleted.";
		}

		public static class StoryReportErrors
		{
			public const string BannedUserReports = "You\'re banned. You have no permission to report.";
			public const string AdminPermission = "Only administrators have a permission to get all reports.";
			public const string ReportNotFoundOrDeleted = "Report not found or it is deleted.";

		}
	}
}
