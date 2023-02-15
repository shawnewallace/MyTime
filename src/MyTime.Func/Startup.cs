
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

[assembly: FunctionsStartup(typeof(MyTime.Func.Startup))]
namespace MyTime.Func
{

	public class Startup : FunctionsStartup
	{
		public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
		{
			// var context = builder.GetContext();

			// builder.ConfigurationBuilder
			// 	.AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
			// 	.AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), optional: true, reloadOnChange: false)
			// 	.AddEnvironmentVariables();
		}

		public override void Configure(IFunctionsHostBuilder builder)
		{
			// builder.Services.AddOptions<AppOptions>()
			// 	.Configure<IConfiguration>((settings, configuration) =>
			// 	{
			// 		configuration.GetSection("AppOptions").Bind(settings);
			// 	});
		}
	}

	public class AppOptions
	{
		public string BlobStorageConnectionString { get; set; }
		public string BlobStorageContainerName { get; set; }
	}
}