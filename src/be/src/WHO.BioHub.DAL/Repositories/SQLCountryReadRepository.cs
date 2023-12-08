using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Countries;

namespace WHO.BioHub.DAL.Repositories;

public class SQLCountryReadRepository : ICountryReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLCountryReadRepository(BioHubDbContext dbContext)
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