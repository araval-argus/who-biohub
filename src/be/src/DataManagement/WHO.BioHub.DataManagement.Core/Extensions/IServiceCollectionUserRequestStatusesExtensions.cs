using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.CreateUserRequestStatus;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.DeleteUserRequestStatus;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ListUserRequestStatuses;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;
using WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.UpdateUserRequestStatus;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionUserRequestStatusesExtensions
{
    public static IServiceCollection AddCoreUserRequestStatuses(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateUserRequestStatusHandler, CreateUserRequestStatusHandler>()
            .AddScoped<ICreateUserRequestStatusMapper, CreateUserRequestStatusMapper>()
            .AddScoped<CreateUserRequestStatusCommandValidator>()

            .AddScoped<IReadUserRequestStatusHandler, ReadUserRequestStatusHandler>()
            .AddScoped<ReadUserRequestStatusQueryValidator>()

            .AddScoped<IReadUserRequestStatusByStatusHandler, ReadUserRequestStatusByStatusHandler>()
            .AddScoped<ReadUserRequestStatusByStatusQueryValidator>()

            .AddScoped<IUpdateUserRequestStatusHandler, UpdateUserRequestStatusHandler>()
            .AddScoped<IUpdateUserRequestStatusMapper, UpdateUserRequestStatusMapper>()
            .AddScoped<UpdateUserRequestStatusCommandValidator>()

            .AddScoped<IDeleteUserRequestStatusHandler, DeleteUserRequestStatusHandler>()
            .AddScoped<DeleteUserRequestStatusCommandValidator>()

            .AddScoped<IListUserRequestStatusesHandler, ListUserRequestStatusesHandler>()
            .AddScoped<ListUserRequestStatusesQueryValidator>()
            ;

        return services;
    }
}