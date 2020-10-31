﻿namespace CampfireStories.Server.Data.Models.Common
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
	{
		[Required]
		public bool IsDeleted { get; set; }
		public DateTime? DeletedOn { get; set; }
	}
}
