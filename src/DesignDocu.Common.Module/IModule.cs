using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesignDocu.Common.Module;

public interface IModule
{
    string ModuleName { get; }
    
    void ConfigureServices(IServiceCollection services, IConfigurationSection configuration);
 
    void ConfigureEndpoints(WebApplication application);
}