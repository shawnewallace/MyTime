using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.Infrastructure;
using MyTime.Persistence;
using FluentValidation;

namespace MyTime.Fns.Configuration;

public interface IServiceInstaller
{
	void Install(IServiceCollection services, IConfiguration configuration);
}

public class ApplicationServiceInstaller : IServiceInstaller
{
	public void Install(IServiceCollection services, IConfiguration configuration)
	{
		services.AddLogging();


		// Add MediatR and load handlers from Lib project
		var cqrsAssembly = typeof(CreateNewEntryCommandHandler).GetTypeInfo().Assembly;
		services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(cqrsAssembly));
		services.AddValidatorsFromAssembly(cqrsAssembly);

		// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
		// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
	}
}

public class InfrastructureServiceInstaller : IServiceInstaller
{
	public void Install(IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<MyTimeSqlDbContext>(
					options => options.UseSqlServer(
							Environment.GetEnvironmentVariable("MyTimeSqlDbContextConnectionString"))
			);
	}
}

public static class DependencyInjection
{
	public static IServiceCollection InstallServices(
		this IServiceCollection services,
		IConfiguration configuration,
		params Assembly[] assemblies)
	{
		IEnumerable<IServiceInstaller> serviceInstallers = assemblies
			.SelectMany(a => a.DefinedTypes)
			.Where(IsAssignableToType<IServiceInstaller>)
			.Select(Activator.CreateInstance)
			.Cast<IServiceInstaller>();

		foreach (IServiceInstaller serviceInstaller in serviceInstallers)
		{
			serviceInstaller.Install(services, configuration);
		}

		return services;

		static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
						typeof(T).IsAssignableFrom(typeInfo) &&
						!typeInfo.IsInterface &&
						!typeInfo.IsAbstract;
	}
}