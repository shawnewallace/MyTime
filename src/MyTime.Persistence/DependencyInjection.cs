using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MyTime.Persistence;

public static class DependencyInjection
{
  public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
  {
    System.Reflection.Assembly assembly = typeof(DependencyInjection).Assembly;

    services.AddDbContext<MyTimeSqlDbContext>(
          options =>
          {
            options.UseSqlServer(
                          connectionString,
                          optionsBuilder => optionsBuilder.MigrationsAssembly("MyTime.Api"));
            options.EnableDetailedErrors();
          });

    return services;
  }
}
