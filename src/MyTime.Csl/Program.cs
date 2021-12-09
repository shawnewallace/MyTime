using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyTime.Persistence;

namespace MyTime.Csl
{
	class Program
	{
		public static async Task Main(string[] args)
		{
			await Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) =>
				{
					var config = hostContext.Configuration;
					services.AddHostedService<ConsoleHostedService>();
					services.AddDbContext<MyTimeSqlDbContext>(options => options.UseSqlServer(config.GetSection("ConnectionStrings")["MyTimeConnectionString"]));
				})
				.RunConsoleAsync();
		}
	}

	internal class ConsoleHostedService : IHostedService
	{
		private int? _exitCode;
		private readonly ILogger _logger;
		private readonly IHostApplicationLifetime _appLifetime;

		public ConsoleHostedService(
				ILogger<ConsoleHostedService> logger,
				IHostApplicationLifetime appLifetime)
		{
			_logger = logger;
			_appLifetime = appLifetime;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

			_appLifetime.ApplicationStarted.Register(() =>
			{
				Task.Run(async () =>
					{
						try
						{
							_logger.LogInformation("Hello World!");
							await Task.Delay(1000);
							_exitCode = 0;
						}
						catch (Exception ex)
						{
							_logger.LogError(ex, "Unhandled exception!");
						}
						finally
						{
							_appLifetime.StopApplication();
							_exitCode = 1;
						}
					});
			});

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogDebug($"Exiting with return code: {_exitCode}");

			// Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
			Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
			return Task.CompletedTask;
		}
	}
}
