
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
		}
	}
}
