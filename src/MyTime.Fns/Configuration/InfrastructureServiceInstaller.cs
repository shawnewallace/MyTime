using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTime.Persistence;

namespace MyTime.Fns.Configuration;

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
