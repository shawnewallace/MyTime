using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTime.App.Entries.CreateNewEntry;
using MyTime.App.Infrastructure;
using FluentValidation;
using MediatR.Pipeline;

namespace MyTime.Functions.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
	public void Install(IServiceCollection services, IConfiguration configuration)
	{
		services.AddLogging();

		// Add MediatR and load handlers from Lib project
		var cqrsAssembly = typeof(CreateNewEntryCommandHandler).GetTypeInfo().Assembly;
		services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(cqrsAssembly));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
		services.AddValidatorsFromAssembly(cqrsAssembly, includeInternalTypes: true);
	}
}
