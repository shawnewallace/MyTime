using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTime.Functions;
using MyTime.Functions.Configuration;

var host = new HostBuilder()
		.ConfigureFunctionsWorkerDefaults(builder =>
		{
			var configuration = builder.Services
				.BuildServiceProvider()
				.GetService<IConfiguration>();

			builder.Services
			.AddOptions<MyOptions>()
			.Configure<IConfiguration>((settings, configuration) =>
				{
					configuration.GetSection(nameof(MyOptions)).Bind(settings);
				});

			builder.Services
				.InstallServices(configuration, typeof(IServiceInstaller).Assembly);
		})
		.Build();

host.Run();
