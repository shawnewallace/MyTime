using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
		.ConfigureFunctionsWorkerDefaults(builder =>
		{
			var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

			builder.Services
			.AddOptions<MyOptions>()
			.Configure<IConfiguration>((settings, configuration) =>
				{
					configuration.GetSection(nameof(MyOptions)).Bind(settings);
				});
		})
		.Build();

host.Run();


public class MyOptions
{
	public string BlobStorageConnectionString { get; set; }
	public string BlobStorageContainerName { get; set; }
}