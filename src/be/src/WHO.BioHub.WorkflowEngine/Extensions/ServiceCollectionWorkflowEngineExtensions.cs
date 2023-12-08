using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.WorkflowEngine.Extensions
{
    public static class ServiceCollectionWorkflowEngineExtensions
    {
        public static IServiceCollection AddBioHubWorkflowEngine(this IServiceCollection services, ApplicationConfiguration workflowEngineConfiguration)
        {
            services
                .AddScoped<IWorklistToBioHubEngine, WorklistToBioHubEngine>()
                .AddScoped<IWorklistFromBioHubEngine, WorklistFromBioHubEngine>()
                .AddScoped<ISMTA1WorkflowEngine, SMTA1WorkflowEngine>()
                .AddScoped<ISMTA2WorkflowEngine, SMTA2WorkflowEngine>()
                .AddScoped<IWorklistToBioHubEmailNotifier, WorklistToBioHubEmailNotifier>()
                .AddScoped<IWorklistFromBioHubEmailNotifier, WorklistFromBioHubEmailNotifier>()
                .AddScoped<ISMTA1WorkflowEmailNotifier, SMTA1WorkflowEmailNotifier>()
                .AddScoped<ISMTA2WorkflowEmailNotifier, SMTA2WorkflowEmailNotifier>()
                .AddScoped<IWorkflowEngineUtility>(_ => new WorkflowEngineUtility(workflowEngineConfiguration));

            return services;
        }
    }
}
