
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MediatR.Pipeline;
using MyTime.App.Infrastructure;
using MyTime.App.Entries.CreateNewEntry;
using System.Reflection;
using MyTime.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using MyTime.Fns.Configuration;

[assembly: FunctionsStartup(typeof(MyTime.Fns.Startup))]
namespace MyTime.Fns
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var configuration = builder
				.Services
				.BuildServiceProvider()
				.GetService<IConfiguration>();

			builder.Services
				.AddOptions<MyOptions>()
				.Configure<IConfiguration>((settings, configuration) =>
				{
					configuration.GetSection(nameof(MyOptions)).Bind(settings);
				});

			builder.Services
				.InstallServices(
					configuration,
					typeof(IServiceInstaller).Assembly);
		}
	}
}
