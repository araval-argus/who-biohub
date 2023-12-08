using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ApproveOrRejectUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.CreateUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.DeleteUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ListUserRequests;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ReadUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequests.UpdateUserRequest;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionUserRequestsExtensions
{
    public static IServiceCollection AddCoreUserRequests(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateUserRequestHandler, CreateUserRequestHandler>()
            .AddScoped<ICreateUserRequestMapper, CreateUserRequestMapper>()
            .AddScoped<CreateUserRequestCommandValidator>()

            .AddScoped<IReadUserRequestHandler, ReadUserRequestHandler>()
            .AddScoped<IReadUserRequestMapper,ReadUserRequestMapper>()
            .AddScoped<ReadUserRequestQueryValidator>()

            .AddScoped<IUpdateUserRequestHandler, UpdateUserRequestHandler>()
            .AddScoped<IUpdateUserRequestMapper, UpdateUserRequestMapper>()
            .AddScoped<UpdateUserRequestCommandValidator>()

            .AddScoped<IApproveOrRejectUserRequestHandler, ApproveOrRejectUserRequestHandler>()
            .AddScoped<IApproveOrRejectUserRequestMapper, ApproveOrRejectUserRequestMapper>()
            .AddScoped<ApproveOrRejectUserRequestCommandValidator>()

            .AddScoped<IDeleteUserRequestHandler, DeleteUserRequestHandler>()
            .AddScoped<DeleteUserRequestCommandValidator>()

            .AddScoped<IListUserRequestsHandler, ListUserRequestsHandler>()
            .AddScoped<IListUserRequestsMapper, ListUserRequestsMapper>()
            .AddScoped<ListUserRequestsQueryValidator>()
            ;

        return services;
    }
}