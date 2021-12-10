using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyTime.Persistence;

namespace MyTime.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				try
				{
					var context = scope.ServiceProvider.GetService<MyTimeSqlDbContext>();
					context.Database.Migrate();

					MyTimeSqlDbContextInitializer.Initialize(context);
				}
				catch (Exception ex)
				{
					var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while migrating or initializing the database.");
				}
			}

			host.Run();
		}

		public static IWebHostBuilder CreateHostBuilder(string[] args) =>
				new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					var env = hostingContext.HostingEnvironment;
					config
									.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
									.AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
									.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

					// if (env.IsDevelopment())
					// {
					// 	config.AddUserSecrets<Program>();
					// }
					config.AddEnvironmentVariables();

					// if (!env.IsDevelopment())
					// {
					// 	var settings = config.Build();
					// 	var connStr = settings["ConnectionStrings:AppConfigConnectionString"];
					// 	if (connStr != null)
					// 	{
					// 		config.AddAzureAppConfiguration(options =>
					// 				{
					// 						options
					// 										.Connect(connStr)
					// 										.UseFeatureFlags();
					// 					});
					// 	}
					// }
				})
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
					logging.AddDebug();
				})
				.UseStartup<Startup>();
	}
}
