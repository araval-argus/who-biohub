using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WHO.BioHub.Search.Core.Extensions;

public interface IInjector
{
    IServiceCollection InjectDependencies(IServiceCollection services);
}

public static class InjectorExecutor
{
    public static void Inject(IServiceCollection services)
    {
        IEnumerable<TypeInfo>? types = Assembly.GetAssembly(typeof(InjectorExecutor))?.DefinedTypes
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