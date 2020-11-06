namespace CampfireStories.Server.Infrastructure
{
	using CampfireStories.Server.Data.Models.Common;
	using Data;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

	public static class ApplicationBuilderExtensions
	{
		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using var services = app.ApplicationServices.CreateScope();

			var dbContext = services.ServiceProvider.GetService<CampfireStoriesDbContext>();

			dbContext.Database.Migrate();
			new DbContextSeeder().SeedAsync(dbContext, services.ServiceProvider).GetAwaiter().GetResult();
		}

		public static IApplicationBuilder UseSwaggerUI (this IApplicationBuilder app)
		{
			app.UseSwagger()
				.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "My CampfireStories API");
					options.RoutePrefix = "swagger";

				});

			return app;
		}
	}
}
