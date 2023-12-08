using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLUserRequestPublicWriteRepository : IUserRequestPublicWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<UserRequest> UserRequests => _dbContext.UserRequests;

    public SQLUserRequestPublicWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<UserRequest, Errors>> Create(UserRequest userRequest, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(userRequest, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(userRequest);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        UserRequest lab = await UserRequests.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        UserRequests.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<UserRequest> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.UserRequests
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(UserRequest userRequest, CancellationToken cancellationToken)
    {
        UserRequests.Update(userRequest);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}