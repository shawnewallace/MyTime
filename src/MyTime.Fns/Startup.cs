
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

[assembly: FunctionsStartup(typeof(MyTime.Fns.Startup))]
namespace MyTime.Fns
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			builder.Services.AddOptions<MyOptions>()
				.Configure<IConfiguration>((settings, configuration) =>
				{
					configuration.GetSection(nameof(MyOptions)).Bind(settings);
				});

			builder.Services.AddLogging();

			// Add MediatR and load handlers from Lib project
			builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
			builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
			builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
			builder.Services.AddMediatR(typeof(CreateNewEntryCommandHandler).GetTypeInfo().Assembly);

			// FOR DEMONSTRATION PURPOSES
			builder.Services.AddDbContext<MyTimeSqlDbContext>(
					options => options.UseSqlServer(
							Environment.GetEnvironmentVariable("MyTimeSqlDbContextConnectionString"))
			);
		}
	}
}
