using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.FunctionApp;

namespace WHO.BioHub.Search.DependencyInjection;

public interface IInjector
{
    IServiceCollection InjectDependencies(IServiceCollection services);
}

public static class InjectorExecutor
{
    public static void Inject(IServiceCollection services)
    {
        IEnumerable<TypeInfo>? types = Assembly.GetAssembly(typeof(Startup))?.DefinedTypes
                    .Where(x => x.ImplementedInterfaces.Any(i => i == typeof(IInjector)));

        IEnumerable<IInjector?> injectors = types
            ?.Select(t => (IInjector?)Activator.CreateInstance(t))
            ?? Array.Empty<IInjector?>();
        foreach (IInjector? injector in injectors)
        {
            injector?.InjectDependencies(services);
        }
    }
}