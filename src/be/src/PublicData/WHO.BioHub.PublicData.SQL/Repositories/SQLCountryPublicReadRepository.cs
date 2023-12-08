using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLCountryPublicReadRepository : ICountryPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLCountryPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Country>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Countries
            .Where(l => l.DeletedOn == null)
            .OrderBy(x => x.Name)
            .ToArrayAsync(cancellationToken);

    }

    public async Task<Country> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Countries
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}