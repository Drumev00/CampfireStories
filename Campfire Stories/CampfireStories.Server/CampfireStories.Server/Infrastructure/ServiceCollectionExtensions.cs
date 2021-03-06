﻿namespace CampfireStories.Server.Infrastructure
{
	using Features.Category;
	using Features.Identity;
	using Data;
	using Data.Models;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.OpenApi.Models;
	using System.Text;
	using CampfireStories.Server.Features.User;
	using CampfireStories.Server.Features.Story;
	using CampfireStories.Server.Features.StoryCategories;
	using CampfireStories.Server.Features.Comment;
	using CampfireStories.Server.Features.SubComments;
	using CampfireStories.Server.Features.StoryReports;
	using CampfireStories.Server.Features.UserReports;
	using CampfireStories.Server.Features.Rating;

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

		public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddDbContext<CampfireStoriesDbContext>(options => options
					.UseSqlServer(configuration.GetDefaultConnectionString()));

			return services;
		}

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			return services
				.AddTransient<IIdentityService, IdentityService>()
				.AddTransient<ICategoriesService, CategoriesService>()
				.AddTransient<IUsersService, UsersService>()
				.AddTransient<IStoriesService, StoriesService>()
				.AddTransient<IStoryCategoriesService, StoryCategoriesService>()
				.AddTransient<ICommentsService, CommentsService>()
				.AddTransient<ISubCommentsService, SubCommentsService>()
				.AddTransient<IStoryReportsService, StoryReportsService>()
				.AddTransient<IUserReportsService, UserReportsService>()
				.AddTransient<IRatingsService, RatingsService>();
		}
		

		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(
					"v1",
					new OpenApiInfo
					{
						Title = "My CampfireStories API",
						Version = "v1"
					});
			});

			return services;
		}
	}
}
