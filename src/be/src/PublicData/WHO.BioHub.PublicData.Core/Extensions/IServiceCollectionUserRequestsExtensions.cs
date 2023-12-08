using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.UserRequests.ReadUserRequest;
using WHO.BioHub.PublicData.Core.UseCases.UserRequests.CreateUserRequest;
using WHO.BioHub.PublicData.Core.UseCases.UserRequests.UpdateUserRequest;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionUserRequestsExtensions
{
    public static IServiceCollection AddCoreUserRequests(this IServiceCollection services)
    {
        services
            .AddScoped<IReadUserRequestHandler, ReadUserRequestHandler>()
            .AddScoped<ReadUserRequestQueryValidator>()
            .AddScoped<ICreateUserRequestHandler, CreateUserRequestHandler>()
            .AddScoped<CreateUserRequestCommandValidator>()
            .AddScoped<ICreateUserRequestMapper, CreateUserRequestMapper>()
            .AddScoped<IUpdateUserRequestHandler, UpdateUserRequestHandler>()
            .AddScoped<UpdateUserRequestCommandValidator>()
            .AddScoped<IUpdateUserRequestMapper, UpdateUserRequestMapper>();


        return services;
    }
}