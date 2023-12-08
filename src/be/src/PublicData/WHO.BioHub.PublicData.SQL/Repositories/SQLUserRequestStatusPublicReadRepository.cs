using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLUserRequestStatusPublicReadRepository : IUserRequestStatusPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLUserRequestStatusPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
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