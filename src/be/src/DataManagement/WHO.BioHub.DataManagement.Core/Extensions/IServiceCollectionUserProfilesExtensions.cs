using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUser;
using WHO.BioHub.DataManagement.Core.UseCases.Users.CreateUserFromUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.Users.DeleteUser;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsers;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUserByExternalId;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;
using WHO.BioHub.DataManagement.Core.UseCases.Users.UpdateUser;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;
using WHO.BioHub.DataManagement.Core.UseCases.Users.ListCourierUsers;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionUsersExtensions
{
    public static IServiceCollection AddCoreUsers(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateUserHandler, CreateUserHandler>()
            .AddScoped<ICreateUserMapper, CreateUserMapper>()
            .AddScoped<CreateUserCommandValidator>()

            .AddScoped<IReadUserHandler, ReadUserHandler>()
            .AddScoped<IReadUserMapper, ReadUserMapper>()
            .AddScoped<ReadUserQueryValidator>()

            .AddScoped<IListUsersByLaboratoryIdForWorklistToBioHubItemHandler, ListUsersByLaboratoryIdForWorklistToBioHubItemHandler>()
            .AddScoped<ListUsersByLaboratoryIdForWorklistToBioHubItemQueryValidator>()

            .AddScoped<IListUsersByBioHubFacilityIdForWorklistToBioHubItemHandler, ListUsersByBioHubFacilityIdForWorklistToBioHubItemHandler>()
            .AddScoped<ListUsersByBioHubFacilityIdForWorklistToBioHubItemQueryValidator>()

             .AddScoped<IListCourierUsersForWorklistToBioHubItemHandler, ListCourierUsersForWorklistToBioHubItemHandler>()
            .AddScoped<ListCourierUsersForWorklistToBioHubItemQueryValidator>()

            .AddScoped<IReadUserByExternalIdHandler, ReadUserByExternalIdHandler>()
            .AddScoped<ReadUserByExternalIdQueryValidator>()

            .AddScoped<IUpdateUserHandler, UpdateUserHandler>()
            .AddScoped<IUpdateUserMapper, UpdateUserMapper>()
            .AddScoped<UpdateUserCommandValidator>()

            .AddScoped<IDeleteUserHandler, DeleteUserHandler>()
            .AddScoped<DeleteUserCommandValidator>()

            .AddScoped<IListUsersHandler, ListUsersHandler>()
            .AddScoped<IListUsersMapper, ListUsersMapper>()
            .AddScoped<ListUsersQueryValidator>()

            .AddScoped<ICreateUserFromUserRequestHandler, CreateUserFromUserRequestHandler>()
            .AddScoped<ICreateUserFromUserRequestMapper, CreateUserFromUserRequestMapper>()
            .AddScoped<CreateUserFromUserRequestCommandValidator>()

            .AddScoped<IListCourierUsersForWorklistFromBioHubItemHandler, ListCourierUsersForWorklistFromBioHubItemHandler>()
            .AddScoped<ListCourierUsersForWorklistFromBioHubItemQueryValidator>()

            .AddScoped<IListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler, ListUsersByBioHubFacilityIdForWorklistFromBioHubItemHandler>()
            .AddScoped<ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQueryValidator>()

            .AddScoped<IListUsersByLaboratoryIdForWorklistFromBioHubItemHandler, ListUsersByLaboratoryIdForWorklistFromBioHubItemHandler>()
            .AddScoped<ListUsersByLaboratoryIdForWorklistFromBioHubItemQueryValidator>()
            ;

        return services;
    }
}