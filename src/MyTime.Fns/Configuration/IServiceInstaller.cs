using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyTime.Fns.Configuration;

public interface IServiceInstaller
{
	void Install(IServiceCollection services, IConfiguration configuration);
}
