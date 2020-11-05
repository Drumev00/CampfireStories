namespace CampfireStories.Server.Data.Models
{
	using System;
	using Microsoft.AspNetCore.Identity;

	using Data.Models.Common;

	public class Role : IdentityRole, IAuditInfo, IDeletableEntity
	{

		public Role()
			: this(null)
		{
				
		}
		public Role(string name)
			: base(name)
		{
			Id = Guid.NewGuid().ToString();
			CreatedOn = DateTime.UtcNow;
			IsDeleted = false;
		}

		public bool IsDeleted { get; set; }
		public DateTime? DeletedOn { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}
