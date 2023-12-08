using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLUserRequestStatusWriteRepository : IUserRequestStatusWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<UserRequestStatus> UserRequestStatuses => _dbContext.UserRequestStatuses;

    public SQLUserRequestStatusWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<UserRequestStatus, Errors>> Create(UserRequestStatus userRequestStatus, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(userRequestStatus, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(userRequestStatus);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        UserRequestStatus lab = await UserRequestStatuses.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        UserRequestStatuses.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<UserRequestStatus> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.UserRequestStatuses
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(UserRequestStatus userRequestStatus, CancellationToken cancellationToken)
    {
        UserRequestStatuses.Update(userRequestStatus);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}