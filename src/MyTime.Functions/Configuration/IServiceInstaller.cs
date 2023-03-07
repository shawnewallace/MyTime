using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyTime.Functions.Configuration;

public interface IServiceInstaller
{
	void Install(IServiceCollection services, IConfiguration configuration);
}
