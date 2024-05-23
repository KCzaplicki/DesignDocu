using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesignDocu.Common.Module;

public static class ModuleInitializer
{
    private static readonly IList<IModule> RegisteredModules = [];
    
    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfigurationSection configuration)
    {
        var modules = GetModules();

        foreach (var module in modules)
        {
            var moduleConfiguration = configuration.GetSection(module.ModuleName);
            if (moduleConfiguration.Exists())
            {
                var moduleConfig = moduleConfiguration.Get<ModuleConfiguration>();
                if (moduleConfig?.Enabled == true)
                {
                    RegisteredModules.Add(module);
                    module.ConfigureServices(services, moduleConfiguration);
                }
            }
        }
        
        return services;
    }
    
    public static WebApplication UseModules(this WebApplication app)
    {
        foreach (var module in RegisteredModules)
        {
            module.Configure(app);
        }
        
        return app;
    }
    
    private static IEnumerable<IModule> GetModules()
    {
        
        var interfaceType = typeof(IModule);
        var implementingTypes = new List<Type>();

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var allAssemblies = Directory
            .GetFiles(path, "*.dll")
            .Select(dll => Assembly.LoadFile(dll)).ToList();
        var assemblies = allAssemblies
            .Where(a => a.GetName().Name.StartsWith(nameof(DesignDocu)));

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            implementingTypes.AddRange(types.Where(type => interfaceType.IsAssignableFrom(type) && type.IsClass));
        }
        
        return implementingTypes.Select(moduleType => (IModule)Activator.CreateInstance(moduleType)!).ToList();
    }
}