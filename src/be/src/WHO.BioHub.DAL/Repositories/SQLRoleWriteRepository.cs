using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Roles;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLRoleWriteRepository : IRoleWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<Role> Roles => _dbContext.Roles;

    public SQLRoleWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<Role, Errors>> Create(Role role, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(role, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(role);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        Role lab = await Roles.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Roles.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Role> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Roles
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(Role role, CancellationToken cancellationToken)
    {
        Roles.Update(role);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}