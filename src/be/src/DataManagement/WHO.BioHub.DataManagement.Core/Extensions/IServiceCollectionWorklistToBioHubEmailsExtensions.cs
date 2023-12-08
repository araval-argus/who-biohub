using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.CreateWorklistToBioHubEmail;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.DeleteWorklistToBioHubEmail;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ListWorklistToBioHubEmails;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ReadWorklistToBioHubEmail;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.UpdateWorklistToBioHubEmail;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionWorklistToBioHubEmailsExtensions
{
    public static IServiceCollection AddCoreWorklistToBioHubEmails(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateWorklistToBioHubEmailHandler, CreateWorklistToBioHubEmailHandler>()
            .AddScoped<ICreateWorklistToBioHubEmailMapper, CreateWorklistToBioHubEmailMapper>()
            .AddScoped<CreateWorklistToBioHubEmailCommandValidator>()

            .AddScoped<IReadWorklistToBioHubEmailHandler, ReadWorklistToBioHubEmailHandler>()
            .AddScoped<ReadWorklistToBioHubEmailQueryValidator>()

            .AddScoped<IUpdateWorklistToBioHubEmailHandler, UpdateWorklistToBioHubEmailHandler>()
            .AddScoped<IUpdateWorklistToBioHubEmailMapper, UpdateWorklistToBioHubEmailMapper>()
            .AddScoped<UpdateWorklistToBioHubEmailCommandValidator>()

            .AddScoped<IDeleteWorklistToBioHubEmailHandler, DeleteWorklistToBioHubEmailHandler>()
            .AddScoped<DeleteWorklistToBioHubEmailCommandValidator>()

            .AddScoped<IListWorklistToBioHubEmailsHandler, ListWorklistToBioHubEmailsHandler>()
            .AddScoped<ListWorklistToBioHubEmailsQueryValidator>()
            ;

        return services;
    }
}