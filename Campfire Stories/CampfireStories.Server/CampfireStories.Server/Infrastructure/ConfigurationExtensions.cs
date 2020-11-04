namespace CampfireStories.Server.Infrastructure
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public static class ConfigurationExtensions
	{
		public static string GetDefaultConnectionString(this IConfiguration configuration)
			=> configuration.GetConnectionString("DefaultConnection");

		public static AppSettings GetAppSettings (this IServiceCollection services, IConfiguration configuration)
		{
			var appSettingsConfig = configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsConfig);
			return appSettingsConfig.Get<AppSettings>();
		}
	}
}
