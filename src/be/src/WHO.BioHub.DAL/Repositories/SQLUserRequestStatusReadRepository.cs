using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLUserRequestStatusReadRepository : IUserRequestStatusReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLUserRequestStatusReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserRequestStatus>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.UserRequestStatuses
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<UserRequestStatus> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.UserRequestStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<UserRequestStatus> ReadByStatus(UserRegistrationStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.UserRequestStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Status == status, cancellationToken);
    }
}