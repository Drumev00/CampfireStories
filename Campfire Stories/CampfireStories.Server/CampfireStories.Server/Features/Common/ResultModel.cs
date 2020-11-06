namespace CampfireStories.Server.Features.Common
{
	using System.Collections.Generic;

	public class ResultModel<T>
	{
		public ResultModel()
		{
			this.Errors = new List<string>();
		}

		public T Result { get; set; }

		public bool Success { get; set; }

		public ICollection<string> Errors { get; set; }
	}
}
