namespace CampfireStories.Server
{
	using Infrastructure;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var appSettings = services.GetAppSettings(this.Configuration);

			services
				.AddDatabase(this.Configuration)
				.AddIdentity()
				.AddApplicationServices()
				.AddSwagger()
				.AddJwtAuthentication(appSettings)
				.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting()
				.UseSwaggerUI()
				.UseCors(options => options
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod())
				.UseAuthentication()
				.UseAuthorization()
				.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
				})
				.ApplyMigrations();
		}
	}
}
