using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;

namespace WHO.BioHub.DAL.Repositories;

public class SQLUserRequestReadRepository : IUserRequestReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLUserRequestReadRepository(BioHubDbContext dbContext)
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
}