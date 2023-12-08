using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Models.Repositories.Users;

public interface IUserWriteRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<Either<User, Errors>> Create(User user, CancellationToken cancellationToken);
    Task<Errors?> CreateUserHistoryItem(User user, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Errors?> DeleteUsersByBioHubFacility(Guid bioHubFacilityId, Guid? operatorId, DateTime deletedOn, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<Errors?> DeleteUsersByLaboratory(Guid laboratoryId, Guid? operatorId, DateTime deletedOn, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    Task<User> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(User user, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
}
