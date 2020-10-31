namespace CampfireStories.Server
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.EntityFrameworkCore;
	using CampfireStories.Server.Data;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using CampfireStories.Server.Data.Models;
	using System.Text;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.IdentityModel.Tokens;

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
			services
				.AddDbContext<CampfireStoriesDbContext>(options => options
					.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<CampfireStoriesDbContext>();


			services.AddControllers();

			var appSettingsConfig = this.Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsConfig);

			var appSettings = appSettingsConfig.Get<AppSettings>();
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
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}

			app.UseRouting()
				.UseCors(options => options
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod())
				.UseAuthentication()
				.UseAuthorization()
				.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
				}); ;
		}
	}
}
