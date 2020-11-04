﻿namespace CampfireStories.Server.Infrastructure
{
	using Data;
	using Data.Models;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.IdentityModel.Tokens;
	using System.Text;

	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddIdentity(this IServiceCollection services)
		{
			services.AddIdentity<User, Role>(options =>
			{
				options.Password.RequiredLength = 6;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
			})
				.AddEntityFrameworkStores<CampfireStoriesDbContext>();

			return services;
		}

		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
		{
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			return services;
		}
	}
}
