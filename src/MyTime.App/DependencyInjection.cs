using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using MyTime.App.Infrastructure;

namespace MyTime.App;

public static class DependencyInjection
{
	public static IServiceCollection AddApp(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogginPipelineBehavior<,>));

		services.AddMediatR(configuration =>
			configuration.RegisterServicesFromAssembly(assembly)
		);

		services.AddValidatorsFromAssembly(assembly);

		return services;
	}
}