using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Users;

public interface IUserReadRepository
{
    Task<User> Read(Guid id, bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<IEnumerable<User>> ListUsersByLaboratoryId(Guid laboratoryId, bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<User> ReadByExternalId(Guid externalId, CancellationToken cancellationToken);
    Task<User> ReadByEmail(string email, CancellationToken cancellationToken);
    Task<IEnumerable<User>> List(bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<bool> EmailPresent(string email, CancellationToken cancellationToken);
    Task<IEnumerable<User>> ListByPermissionName(string permissionName, Guid? laboratoryId, Guid? bioHubFacilityId, CancellationToken cancellationToken);
    Task<IEnumerable<User>> ListUsersByBioHubFacilityId(Guid bioHubFacilityId, bool excludeOnBehalfOf, CancellationToken cancellationToken, bool? onlyFocalPoints = true);
    Task<IEnumerable<User>> ListUsersByCourierId(Guid courierId, CancellationToken cancellationToken);
    Task<IEnumerable<User>> ListCourierUsers(CancellationToken cancellationToken);
    Task<IEnumerable<User>> ListForLaboratoryUser(Guid laboratoryId, bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<IEnumerable<User>> ListForBioHubFacilityUser(Guid bioHubFacilityId, bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<User> ReadCourierUser(Guid id, CancellationToken cancellationToken);
    Task<User> ReadForLaboratoryUser(Guid id, Guid laboratoryId, bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<User> ReadForBioHubFacilityUser(Guid id, Guid bioHubFacilityId, bool excludeOnBehalfOf, CancellationToken cancellationToken);
    Task<IEnumerable<User>> ListCourierUsersForWorklist(CancellationToken cancellationToken);
    Task<User> ReadByEmailAuth(string email, Guid externalId, CancellationToken cancellationToken);
    Task<UserHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken);
}
