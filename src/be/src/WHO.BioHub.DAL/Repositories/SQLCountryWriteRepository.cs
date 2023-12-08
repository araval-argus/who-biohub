using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLCountryWriteRepository : ICountryWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<Country> Countries => _dbContext.Countries;

    public SQLCountryWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<Country, Errors>> Create(Country country, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(country, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(country);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        Country lab = await Countries.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        Countries.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<Country> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Countries
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(Country country, CancellationToken cancellationToken)
    {
        Countries.Update(country);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}