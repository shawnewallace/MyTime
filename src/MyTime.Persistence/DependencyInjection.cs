using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyTime.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddDbContext<MyTimeSqlDbContext>(
					options => options.UseSqlServer(
							connectionString,
							optionsBuilder => optionsBuilder.MigrationsAssembly("MyTime.Api"))
			);

		return services;
	}
}