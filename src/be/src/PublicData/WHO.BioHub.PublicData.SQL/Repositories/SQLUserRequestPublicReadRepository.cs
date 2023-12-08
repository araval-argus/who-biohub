using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLUserRequestPublicReadRepository : IUserRequestPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLUserRequestPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserRequest>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.UserRequests
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<UserRequest> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.UserRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<bool> EmailPresent(string email, CancellationToken cancellationToken)
    {
        var presentAmongUsers = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(l => l.DeletedOn == null && l.Email == email, cancellationToken);

        if (presentAmongUsers)
        {
            return true;
        }

        return await _dbContext.UserRequests
            .AsNoTracking()
            .AnyAsync(l => l.DeletedOn == null && l.Email == email && l.Status == UserRegistrationStatus.Pending, cancellationToken);

    }
}